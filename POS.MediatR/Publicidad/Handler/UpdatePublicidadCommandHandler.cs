using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Publicidad.Commands;
using POS.Repository;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Publicidad.Handlers
{
    public class UpdatePublicidadCommandHandler : IRequestHandler<UpdatePublicidadCommand, ServiceResponse<PublicidadDto>>
    {
        private readonly IPublicidadRepository _publicidadRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePublicidadCommandHandler> _logger;
        public UpdatePublicidadCommandHandler(
           IPublicidadRepository publicidadRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<UpdatePublicidadCommandHandler> logger
            )
        {
            _publicidadRepository = publicidadRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<PublicidadDto>> Handle(UpdatePublicidadCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _publicidadRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            
            if (entityExist == null)
            {
                return ServiceResponse<PublicidadDto>.Return404("La publicidad no existe");
            }

            var oldBannerUrl = entityExist.UrlBanner;
            entityExist = _mapper.Map(request, entityExist);

            _publicidadRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<PublicidadDto>.Return500();
            }

            await FileManager.UpdateFile(request.IsBannerChanged, _pathHelper.PublicidadImagePath, entityExist.UrlBanner, request.UrlBannerData, oldBannerUrl);
            
            return ServiceResponse<PublicidadDto>.ReturnResultWith200(_mapper.Map<PublicidadDto>(entityExist));
        }
    }
}
