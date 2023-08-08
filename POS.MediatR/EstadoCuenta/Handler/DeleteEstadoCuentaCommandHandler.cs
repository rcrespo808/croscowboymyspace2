using AutoMapper;
using MediatR;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.EstadoCuenta.Command;
using POS.Repository.EstadoCuenta;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.EstadoCuenta.Handler
{
    public class DeleteEstadoCuentaCommandHandler : IRequestHandler<DeleteEstadoCuentaCommand, ServiceResponse<bool>>
    {
        private readonly IEstadoCuentaRepository _estadoCuentaRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        public DeleteEstadoCuentaCommandHandler(IEstadoCuentaRepository estadoCuentaRepository,
            PathHelper pathHelper,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow
            )
        {
            _estadoCuentaRepository = estadoCuentaRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteEstadoCuentaCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _estadoCuentaRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                return ServiceResponse<bool>.Return404();
            }

            _estadoCuentaRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
