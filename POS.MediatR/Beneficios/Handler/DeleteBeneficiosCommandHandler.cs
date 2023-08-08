using MediatR;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Beneficios.Command;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Beneficios.Handler
{
    public class DeleteBeneficiosCommandHandler
    : IRequestHandler<DeleteBeneficioCommand, ServiceResponse<bool>>
    {

        private readonly IBeneficiosRepository _beneficioRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly ILogger<DeleteBeneficiosCommandHandler> _logger;

        public DeleteBeneficiosCommandHandler(
            IBeneficiosRepository beneficioRepository,
            IUnitOfWork<POSDbContext> uow,
            ILogger<DeleteBeneficiosCommandHandler> logger)
        {
            _beneficioRepository = beneficioRepository;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteBeneficioCommand request, CancellationToken cancellationToken)
        {
            var existingBeneficios = await _beneficioRepository.FindAsync(request.Id);
            if (existingBeneficios == null)
            {
                return ServiceResponse<bool>.Return404("El beneficio no existe");
            }

            _beneficioRepository.Remove(existingBeneficios);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error mientras eliminamos el beneficio");
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
