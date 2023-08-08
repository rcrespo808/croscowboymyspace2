using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.Unit.Commands;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Unit.Handlers
{
    public class GetUnitCommandHandler : IRequestHandler<GetUnitCommand, ServiceResponse<UnitDto>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUnitCommandHandler> _logger;
        public GetUnitCommandHandler(
           IUnitRepository unitRepository,
            IMapper mapper,
            ILogger<GetUnitCommandHandler> logger
            )
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<UnitDto>> Handle(GetUnitCommand request, CancellationToken cancellationToken)
        {
            var entityDto = await _unitRepository.FindBy(c => c.Id == request.Id)
               .ProjectTo<UnitDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync();
            if (entityDto == null)
            {
                _logger.LogError("Unit is not available");
                return ServiceResponse<UnitDto>.Return404();
            }
            return ServiceResponse<UnitDto>.ReturnResultWith200(entityDto);
        }
    }
}
