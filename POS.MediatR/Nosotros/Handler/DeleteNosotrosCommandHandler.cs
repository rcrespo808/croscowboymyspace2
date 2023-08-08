using AutoMapper;
using MediatR;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Nosotros.Command;
using POS.Repository;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Country.Handlers
{
    public class DeleteNosotrosCommandHandler : IRequestHandler<DeleteNosotrosCommand, ServiceResponse<bool>>
    {
        private readonly INosotrosRepository _nosotrosRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        public DeleteNosotrosCommandHandler(INosotrosRepository nosotrosRepository,
            PathHelper pathHelper,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow
            )
        {
            _nosotrosRepository = nosotrosRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteNosotrosCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _nosotrosRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                return ServiceResponse<bool>.Return404();
            }
            var oldNosotrosImage = entityExist.ImageUrl;
            _nosotrosRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }

            string contentRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            if (!string.IsNullOrWhiteSpace(oldNosotrosImage)
                && File.Exists(Path.Combine(contentRootPath, _pathHelper.NosotrosImagePath, oldNosotrosImage)))
            {
                FileData.DeleteFile(Path.Combine(contentRootPath, _pathHelper.NosotrosImagePath, oldNosotrosImage));
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
