using AutoMapper;
using MediatR;
using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Nosotros.Command;
using POS.Repository;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Nosotros.Handler
{
    internal class AddNosotrosCommandHandler : IRequestHandler<AddNosotrosCommand, ServiceResponse<bool>>
    {
        private readonly INosotrosRepository _nosotrosRepository;
        private readonly IMapper _mapper;
        private readonly PathHelper _pathHelper;
        private readonly IUnitOfWork<POSDbContext> _uow;

        public AddNosotrosCommandHandler(INosotrosRepository nosotrosRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow)
        {
            _nosotrosRepository = nosotrosRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
        }

        public async Task<ServiceResponse<bool>> Handle(AddNosotrosCommand request, CancellationToken cancellationToken)
        {
            var nosotros = _mapper.Map<Data.Nosotros>(request);

            if (!string.IsNullOrWhiteSpace(request.ImageUrl))
            {
                nosotros.ImageUrl = Guid.NewGuid().ToString() + ".png";
            }

            _nosotrosRepository.Add(nosotros);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }

            if (!string.IsNullOrWhiteSpace(request.ImageUrl))
            {
                var pathToSave = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), _pathHelper.NosotrosImagePath);
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                await FileData.SaveFile(Path.Combine(pathToSave, nosotros.ImageUrl), request.ImageUrl);
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
