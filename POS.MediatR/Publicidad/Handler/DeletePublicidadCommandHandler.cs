using AutoMapper;
using MediatR;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Publicidad.Command;
using POS.Repository;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Country.Handlers
{
    public class DeletePublicidadCommandHandler : IRequestHandler<DeletePublicidadCommand, ServiceResponse<bool>>
    {
        private readonly IPublicidadRepository _publicidadRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        public DeletePublicidadCommandHandler(IPublicidadRepository publicidadRepository,
            PathHelper pathHelper,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow
            )
        {
            _publicidadRepository = publicidadRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<ServiceResponse<bool>> Handle(DeletePublicidadCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _publicidadRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                return ServiceResponse<bool>.Return404();
            }

            _publicidadRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }

            FileManager.DeleteFile(_pathHelper.PublicidadImagePath, entityExist.UrlBanner);

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
