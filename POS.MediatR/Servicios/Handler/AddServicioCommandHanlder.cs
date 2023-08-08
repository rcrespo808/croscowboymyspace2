using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Servicios.Command;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Servicio.Handler
{
    public class AddServicioCommandHanlder
        : IRequestHandler<AddServicioCommand, ServiceResponse<Data.Servicios>>
    {

        private readonly IServiciosRepository _servicioRepository;
        private readonly IMapper _mapper;
        private readonly PathHelper _pathHelper;
        private readonly IUnitOfWork<POSDbContext> _uow;

        public AddServicioCommandHanlder(IServiciosRepository servicioRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow)
        {
            _servicioRepository = servicioRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<ServiceResponse<Data.Servicios>> Handle(AddServicioCommand request, CancellationToken cancellationToken)
        {
            var existingServicio = await _servicioRepository.All
                .FirstOrDefaultAsync(c => c.Nombre == request.Nombre);
            if (existingServicio != null)
            {
                return ServiceResponse<Data.Servicios>.Return409("Existe un servicio con el mismo nombre");
            }

            var servicio = _mapper.Map<Data.Servicios>(request);
            _servicioRepository.Add(servicio);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<Data.Servicios>.Return500();
            }

            await FileManager.SaveFile(servicio.UrlImage, request.UrlImageData, _pathHelper.ServiciosImagePath);

            servicio.UrlImage = FileManager.GetPathFile(servicio.UrlImage, _pathHelper.ServiciosImagePath);
            return ServiceResponse<Data.Servicios>.ReturnResultWith201(servicio);
        }
    }
}
