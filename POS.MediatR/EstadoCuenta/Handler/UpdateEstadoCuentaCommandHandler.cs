using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;
using POS.Helper;
using POS.MediatR.EstadoCuenta.Commands;
using POS.Repository.EstadoCuenta;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.EstadoCuenta.Handlers
{
    public class UpdateEstadoCuentaCommandHandler : IRequestHandler<UpdateEstadoCuentaCommand, ServiceResponse<EstadoCuentaDto>>
    {
        private readonly IEstadoCuentaRepository _estadoCuentaRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEstadoCuentaCommandHandler> _logger;
        public UpdateEstadoCuentaCommandHandler(
           IEstadoCuentaRepository estadoCuentaRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<UpdateEstadoCuentaCommandHandler> logger
            )
        {
            _estadoCuentaRepository = estadoCuentaRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<EstadoCuentaDto>> Handle(UpdateEstadoCuentaCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _estadoCuentaRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            
            if (entityExist == null)
            {
                return ServiceResponse<EstadoCuentaDto>.Return404("No se encontro");
            }

            entityExist = _mapper.Map(request, entityExist);

            _estadoCuentaRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<EstadoCuentaDto>.Return500();
            }

            return ServiceResponse<EstadoCuentaDto>.ReturnResultWith200(_mapper.Map<EstadoCuentaDto>(entityExist));
        }
    }
}
