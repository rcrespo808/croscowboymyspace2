using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.BuzonSugerencias.Command;
using POS.Repository.BuzonSugerencias;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.BuzonSugerencias.Handler
{
    public class GetBuzonSugerenciasQueryHandler : IRequestHandler<GetBuzonSugerenciasQuery, BuzonSugerenciasDto>
    {
        private readonly IBuzonSugerenciasRepository _buzonSugerenciasRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;

        public GetBuzonSugerenciasQueryHandler(
            IBuzonSugerenciasRepository buzonSugerenciasRepository,
            PathHelper pathHelper,
            IMapper mapper)
        {
            _buzonSugerenciasRepository = buzonSugerenciasRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
        }
        public async Task<BuzonSugerenciasDto> Handle(GetBuzonSugerenciasQuery request, CancellationToken cancellationToken)
        {
            var buzonSugerencias = await _buzonSugerenciasRepository
                                        .FindBy(buzonSugerencias => buzonSugerencias.Id == request.Id)
                                        .ProjectTo<BuzonSugerenciasDto>(_mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync();

            return buzonSugerencias;
        }
    }
}
