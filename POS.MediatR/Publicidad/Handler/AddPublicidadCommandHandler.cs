using AutoMapper;
using MediatR;
using POS.Common.UnitOfWork;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Publicidad.Command;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Publicidad.Handler
{
    internal class AddPublicidadCommandHandler : IRequestHandler<AddPublicidadCommand, ServiceResponse<PublicidadDto>>
    {
        private readonly IPublicidadRepository _publicidadRepository;
        private readonly IMapper _mapper;
        private readonly PathHelper _pathHelper;
        private readonly IUnitOfWork<POSDbContext> _uow;

        public AddPublicidadCommandHandler(IPublicidadRepository publicidadRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow)
        {
            _publicidadRepository = publicidadRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
        }

        public async Task<ServiceResponse<PublicidadDto>> Handle(AddPublicidadCommand request, CancellationToken cancellationToken)
        {
            var publicidad = _mapper.Map<Data.Publicidad>(request);

            _publicidadRepository.Add(publicidad);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<PublicidadDto>.Return500();
            }

            await FileManager.SaveFile(publicidad.UrlBanner, request.UrlBannerData, _pathHelper.PublicidadImagePath);

            return ServiceResponse<PublicidadDto>.ReturnResultWith201(_mapper.Map<PublicidadDto>(publicidad));
        }
    }
}
