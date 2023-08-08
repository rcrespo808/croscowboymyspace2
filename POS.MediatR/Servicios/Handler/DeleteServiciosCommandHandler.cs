using MediatR;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Servicios.Command;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Servicios.Handler
{
    public class DeleteServiciosCommandHandler
    : IRequestHandler<DeleteServicioCommand, ServiceResponse<bool>>
    {

        private readonly IServiciosRepository _servicioRepository;
        private readonly PathHelper _pathHelper;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly ILogger<DeleteServiciosCommandHandler> _logger;

        public DeleteServiciosCommandHandler(
            IServiciosRepository servicioRepository,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<DeleteServiciosCommandHandler> logger)
        {
            _servicioRepository = servicioRepository;
            _uow = uow;
            _pathHelper = pathHelper;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteServicioCommand request, CancellationToken cancellationToken)
        {
            var existingServicios = await _servicioRepository.FindAsync(request.Id);
            if (existingServicios == null)
            {
                return ServiceResponse<bool>.Return404("El servicio no existe");
            }

            _servicioRepository.Remove(existingServicios);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error mientras eliminamos el servicio");
                return ServiceResponse<bool>.Return500();
            }

            FileManager.DeleteFile(_pathHelper.ServiciosImagePath, existingServicios.UrlImage);

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
