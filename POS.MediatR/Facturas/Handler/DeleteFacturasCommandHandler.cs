using AutoMapper;
using MediatR;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Facturas.Command;
using POS.Repository.Facturas;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Facturas.Handler
{
    public class DeleteFacturasCommandHandler : IRequestHandler<DeleteFacturasCommand, ServiceResponse<bool>>
    {
        private readonly IFacturasRepository _facturasRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        public DeleteFacturasCommandHandler(IFacturasRepository facturasRepository,
            PathHelper pathHelper,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow
            )
        {
            _facturasRepository = facturasRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteFacturasCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _facturasRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                return ServiceResponse<bool>.Return404();
            }

            _facturasRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
