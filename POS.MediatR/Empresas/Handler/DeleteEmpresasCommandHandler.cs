using AutoMapper;
using MediatR;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Empresas.Command;
using POS.Repository.Empresas;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Empresas.Handler
{
    public class DeleteEmpresasCommandHandler : IRequestHandler<DeleteEmpresasCommand, ServiceResponse<bool>>
    {
        private readonly IEmpresasRepository _empresasRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        public DeleteEmpresasCommandHandler(IEmpresasRepository empresasRepository,
            PathHelper pathHelper,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow
            )
        {
            _empresasRepository = empresasRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteEmpresasCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _empresasRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                return ServiceResponse<bool>.Return404();
            }

            _empresasRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }

            FileManager.DeleteFile(_pathHelper.EmpresasImagePath, entityExist.UrlLogo);

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
