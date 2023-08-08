using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Helper;
using POS.MediatR.EstadoCuenta.Command;
using POS.Repository.EstadoCuenta;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.EstadoCuenta.Handler
{
    public class GetEstadoCuentaQueryHandler : IRequestHandler<GetEstadoCuentaQuery, EstadoCuentaDto>
    {
        private readonly IEstadoCuentaRepository _estadoCuentaRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;

        public GetEstadoCuentaQueryHandler(
            IEstadoCuentaRepository estadoCuentaRepository,
            PathHelper pathHelper,
            IMapper mapper)
        {
            _estadoCuentaRepository = estadoCuentaRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
        }
        public async Task<EstadoCuentaDto> Handle(GetEstadoCuentaQuery request, CancellationToken cancellationToken)
        {
            var estadoCuenta = await _estadoCuentaRepository
                                        .FindBy(estadoCuenta => estadoCuenta.Id == request.Id)
                                        .ProjectTo<EstadoCuentaDto>(_mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync();

            return estadoCuenta;
        }
    }
}
