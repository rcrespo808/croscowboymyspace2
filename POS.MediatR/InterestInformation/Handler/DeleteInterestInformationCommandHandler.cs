using MediatR;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.InterestInformation.Command;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.InterestInformation.Handler
{
    public class DeleteInterestInformationCommandHandler
        : IRequestHandler<DeleteInterestInformationCommand, ServiceResponse<bool>>
    {
        private readonly IInterestInformationRepository _interestInformationRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly ILogger<DeleteInterestInformationCommand> _logger;

        public DeleteInterestInformationCommandHandler(
           IInterestInformationRepository interestInformationRepository,
            IUnitOfWork<POSDbContext> uow,
            ILogger<DeleteInterestInformationCommand> logger
            )
        {
            _interestInformationRepository = interestInformationRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteInterestInformationCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _interestInformationRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Interest Information Does not exists");
                return ServiceResponse<bool>.Return404("Interest Information Does not exists");
            }

            _interestInformationRepository.Delete(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error While Deleting Interest Information.");
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
