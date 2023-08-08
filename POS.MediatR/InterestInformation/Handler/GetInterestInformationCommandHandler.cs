using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Data.Dto;
using POS.Data.Dto.InterestInformation;
using POS.Data.Entities.Lookups;
using POS.Helper;
using POS.MediatR.InterestInformation.Command;
using POS.MediatR.Tax.Commands;
using POS.Repository;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.InterestInformation.Handler
{
    public class GetInterestInformationCommandHandler
        : IRequestHandler<GetInterestInformationCommand, ServiceResponse<InterestInformationDto>>
    {
        private readonly IInterestInformationRepository _interestInformationRepository;
        private readonly IMapper _mapper;
        private readonly PathHelper _pathHelper;
        private readonly ILogger<GetInterestInformationCommand> _logger;
        public GetInterestInformationCommandHandler(
           IInterestInformationRepository interestInformationRepository,
            IMapper mapper,
            ILogger<GetInterestInformationCommand> logger,
            PathHelper pathHelper
            )
        {
            _interestInformationRepository = interestInformationRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _logger = logger;
        }

        public async Task<ServiceResponse<InterestInformationDto>> Handle(GetInterestInformationCommand request, CancellationToken cancellationToken)
        {
            var entityDto = await _interestInformationRepository
                                                    .FindBy(c => c.Id == request.Id)
                                                    .ProjectTo<InterestInformationDto>(_mapper.ConfigurationProvider)
                                                    .FirstOrDefaultAsync();

            if (entityDto == null)
            {
                _logger.LogError("Interest Information is not exists");
                return ServiceResponse<InterestInformationDto>.Return404();
            }
            return ServiceResponse<InterestInformationDto>.ReturnResultWith200(entityDto);
        }
    }
}
