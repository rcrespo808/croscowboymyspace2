using AutoMapper;
using MediatR;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;
using POS.Helper;
using POS.MediatR.EstadoCuenta.Command;
using POS.Repository.EstadoCuenta;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.EstadoCuenta.Handler
{
    public class AddEstadoCuentaCommandHandler : IRequestHandler<AddEstadoCuentaCommand, ServiceResponse<EstadoCuentaDto>>
    {
        private readonly IEstadoCuentaRepository _estadoCuentaRepository;
        private readonly IMapper _mapper;
        private readonly PathHelper _pathHelper;
        private readonly IUnitOfWork<POSDbContext> _uow;

        public AddEstadoCuentaCommandHandler(IEstadoCuentaRepository estadoCuentaRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow)
        {
            _estadoCuentaRepository = estadoCuentaRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
        }

        public async Task<ServiceResponse<EstadoCuentaDto>> Handle(AddEstadoCuentaCommand request, CancellationToken cancellationToken)
        {
            var estadoCuenta = _mapper.Map<Data.EstadoCuenta>(request);

            _estadoCuentaRepository.Add(estadoCuenta);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<EstadoCuentaDto>.Return500();
            }

            return ServiceResponse<EstadoCuentaDto>.ReturnResultWith201(_mapper.Map<EstadoCuentaDto>(estadoCuenta));
        }
    }
}
