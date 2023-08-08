using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.Unit.Commands;
using POS.Repository;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Unit.Handlers
{
   public class GetAllUnitCommandHandler : IRequestHandler<GetAllUnitCommand, List<UnitDto>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;
        public GetAllUnitCommandHandler(
           IUnitRepository unitRepository,
            IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public async Task<List<UnitDto>> Handle(GetAllUnitCommand request, CancellationToken cancellationToken)
        {
            var entities = await _unitRepository.All.ProjectTo<UnitDto>(_mapper.ConfigurationProvider).ToListAsync();
            return entities;
        }
    }
}
