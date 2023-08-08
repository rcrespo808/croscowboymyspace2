using AutoMapper;
using MediatR;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Certificados.Command;
using POS.Repository.Certificados;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Certificados.Handler
{
    public class DeleteCertificadosCommandHandler : IRequestHandler<DeleteCertificadosCommand, ServiceResponse<bool>>
    {
        private readonly ICertificadosRepository _certificadosRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        public DeleteCertificadosCommandHandler(ICertificadosRepository certificadosRepository,
            PathHelper pathHelper,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow
            )
        {
            _certificadosRepository = certificadosRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteCertificadosCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _certificadosRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                return ServiceResponse<bool>.Return404();
            }

            _certificadosRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
