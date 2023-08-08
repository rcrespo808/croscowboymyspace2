using AutoMapper;
using MediatR;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.BuzonSugerencias.Command;
using POS.Repository.BuzonSugerencias;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.BuzonSugerencias.Handler
{
    public class DeleteBuzonSugerenciasCommandHandler : IRequestHandler<DeleteBuzonSugerenciasCommand, ServiceResponse<bool>>
    {
        private readonly IBuzonSugerenciasRepository _buzonSugerenciasRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        public DeleteBuzonSugerenciasCommandHandler(IBuzonSugerenciasRepository buzonSugerenciasRepository,
            PathHelper pathHelper,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow
            )
        {
            _buzonSugerenciasRepository = buzonSugerenciasRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteBuzonSugerenciasCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _buzonSugerenciasRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                return ServiceResponse<bool>.Return404();
            }

            _buzonSugerenciasRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
