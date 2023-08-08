using MediatR;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.BeneficioCategoria.Command;
using POS.MediatR.Beneficios.Command;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.BeneficioCategoria.Handler
{
    public class DeleteBeneficioCategoriaCommandHandler
    : IRequestHandler<DeleteBeneficioCategoriaCommand, ServiceResponse<bool>>
    {

        private readonly IBeneficiosCategoriasRepository _beneficioCategoriaRepository;
        private readonly PathHelper _pathHelper;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly ILogger<DeleteBeneficioCategoriaCommandHandler> _logger;

        public DeleteBeneficioCategoriaCommandHandler(
            IBeneficiosCategoriasRepository beneficioCategoriaRepository,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<DeleteBeneficioCategoriaCommandHandler> logger)
        {
            _beneficioCategoriaRepository = beneficioCategoriaRepository;
            _pathHelper = pathHelper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteBeneficioCategoriaCommand request, CancellationToken cancellationToken)
        {
            var existingBeneficioCategoria = await _beneficioCategoriaRepository.FindAsync(request.Id);
            if (existingBeneficioCategoria == null)
            {
                return ServiceResponse<bool>.Return404("El Beneficio Categoria no existe");
            }

            _beneficioCategoriaRepository.Remove(existingBeneficioCategoria);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error mientras eliminamos el producto");
                return ServiceResponse<bool>.Return500();
            }

            FileManager.DeleteFile(_pathHelper.BeneficioCategoriaImagePath, existingBeneficioCategoria.UrlBanner);

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
