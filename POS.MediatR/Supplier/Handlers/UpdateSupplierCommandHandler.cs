using AutoMapper;
using POS.Common.UnitOfWork;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, ServiceResponse<SupplierDto>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly ILogger<UpdateSupplierCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;

        public UpdateSupplierCommandHandler(ISupplierRepository supplierRepository,
            ILogger<UpdateSupplierCommandHandler> logger,
            IUnitOfWork<POSDbContext> uow,
            IMapper mapper,
              IWebHostEnvironment webHostEnvironment,
              PathHelper pathHelper
            )
        {
            _supplierRepository = supplierRepository;
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
        }

        public async Task<ServiceResponse<SupplierDto>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _supplierRepository.FindBy(c => c.Id != request.Id && c.SupplierName == request.SupplierName.Trim())
                .FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Supplier Name Already Exist for another supplier.");
                return ServiceResponse<SupplierDto>.Return422("Supplier Name Already Exist for another supplier.");
            }

            var entity = await _supplierRepository
              .AllIncluding()
              .FirstOrDefaultAsync(c => c.Id == request.Id);

            var oldBannerUrl = entity.BannerUrl;
            var oldLogoUrl = entity.LogoUrl;

            entity.LogoUrl = FileManager.GetUpdateFile(request.IsLogoUpdated, oldLogoUrl, request.LogoUrlData, "png");
            entity.BannerUrl = FileManager.GetUpdateFile(request.IsBannerUpdated, oldBannerUrl, request.BannerUrlData, "png");

            entity = _mapper.Map(request, entity);

            _supplierRepository.Update(entity);
            if (_uow.Save() <= 0)
            {
                _logger.LogError("Error to Update Supplier");
                return ServiceResponse<SupplierDto>.Return500();
            }

            await FileManager.UpdateFile(request.IsLogoUpdated, _pathHelper.SupplierImagePath, entity.LogoUrl, request.LogoUrlData, oldLogoUrl);
            await FileManager.UpdateFile(request.IsBannerUpdated, _pathHelper.SupplierImagePath, entity.BannerUrl, request.BannerUrlData, oldBannerUrl);

            return ServiceResponse<SupplierDto>.ReturnResultWith200(_mapper.Map<SupplierDto>(entity));
        }
    }
}
