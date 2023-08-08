using MediatR;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.ServicioCategoria.Command;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.ServicioCategoria.Handler
{
    public class DeleteServicioCategoriaCommandHandler
    : IRequestHandler<DeleteServicioCategoriaCommand, ServiceResponse<bool>>
    {

        private readonly IServiciosCategoriasRepository _servicioCategoriaRepository;
        private readonly PathHelper _pathHelper;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly ILogger<DeleteServicioCategoriaCommandHandler> _logger;

        public DeleteServicioCategoriaCommandHandler(
            IServiciosCategoriasRepository servicioCategoriaRepository,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<DeleteServicioCategoriaCommandHandler> logger)
        {
            _servicioCategoriaRepository = servicioCategoriaRepository;
            _pathHelper = pathHelper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteServicioCategoriaCommand request, CancellationToken cancellationToken)
        {
            var existingServicioCategoria = await _servicioCategoriaRepository.FindAsync(request.Id);
            if (existingServicioCategoria == null)
            {
                return ServiceResponse<bool>.Return404("El Servicio Categoria no existe");
            }

            _servicioCategoriaRepository.Remove(existingServicioCategoria);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error mientras eliminamos el Servicio");
                return ServiceResponse<bool>.Return500();
            }

            FileManager.DeleteFile(_pathHelper.ServicioCategoriaImagePath, existingServicioCategoria.UrlImage);

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
