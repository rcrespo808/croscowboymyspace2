using AutoMapper;
using POS.Common.UnitOfWork;
using POS.Data;
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
using System.Threading;
using System.Threading.Tasks;
using POS.Data.Entities.Lookups;

namespace POS.MediatR.Handlers
{
    public class AddSupplierCommandHandler : IRequestHandler<AddSupplierCommand, ServiceResponse<SupplierDto>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly ILogger<AddSupplierCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;

        public AddSupplierCommandHandler(ISupplierRepository supplierRepository,
            ILogger<AddSupplierCommandHandler> logger,
            IUnitOfWork<POSDbContext> uow,
            IMapper mapper,
              IWebHostEnvironment webHostEnvironment,
              PathHelper pathHelper)
        {
            _supplierRepository = supplierRepository;
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
        }

        public async Task<ServiceResponse<SupplierDto>> Handle(AddSupplierCommand request, CancellationToken cancellationToken)
        {
            var entity = await _supplierRepository.FindBy(c => c.SupplierName == request.SupplierName).FirstOrDefaultAsync();
            if (entity != null)
            {
                _logger.LogError("Socio de Negocio ya existe.");
                return ServiceResponse<SupplierDto>.Return422("Socio de Negocio ya existe.");
            }
            entity = _mapper.Map<Data.Supplier>(request);
            _supplierRepository.Add(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error al guardar Socio de Negocio");
                return ServiceResponse<SupplierDto>.Return500();
            }

            await FileManager.SaveFile(entity.BannerUrl, request.BannerUrlData, _pathHelper.SupplierImagePath);
            await FileManager.SaveFile(entity.LogoUrl, request.LogoUrlData, _pathHelper.SupplierImagePath);

            return ServiceResponse<SupplierDto>.ReturnResultWith200(_mapper.Map<SupplierDto>(entity));
        }
    }
}
