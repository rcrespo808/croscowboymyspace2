using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Eventos.Command;
using POS.MediatR.Nosotros.Commands;
using POS.Repository;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Nosotros.Handlers
{
    public class UpdateNosotrosCommandHandler : IRequestHandler<UpdateNosotrosCommand, ServiceResponse<bool>>
    {
        private readonly INosotrosRepository _nosotrosRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateNosotrosCommandHandler> _logger;
        public UpdateNosotrosCommandHandler(
           INosotrosRepository nosotrosRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<UpdateNosotrosCommandHandler> logger
            )
        {
            _nosotrosRepository = nosotrosRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(UpdateNosotrosCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _nosotrosRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();

            entityExist.Title = request.Title;
            entityExist.Description = request.Description;
            entityExist.Location = request.Location;
            var oldImageUrl = entityExist.ImageUrl;
            if (request.IsImageChanged)
            {
                if (!string.IsNullOrEmpty(request.ImageUrl))
                {
                    entityExist.ImageUrl = $"{Guid.NewGuid()}.png";
                }
                else
                {
                    entityExist.ImageUrl = "";
                }
            }

            _nosotrosRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }

            if (request.IsImageChanged)
            {
                string contentRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"); //_webHostEnvironment.WebRootPath;
                // delete old file
                if (!string.IsNullOrWhiteSpace(oldImageUrl)
                    && File.Exists(Path.Combine(contentRootPath, _pathHelper.NosotrosImagePath, oldImageUrl)))
                {
                    FileData.DeleteFile(Path.Combine(contentRootPath, _pathHelper.NosotrosImagePath, oldImageUrl));
                }

                // save new file
                if (!string.IsNullOrWhiteSpace(request.ImageUrl))
                {
                    var pathToSave = Path.Combine(contentRootPath, _pathHelper.NosotrosImagePath);
                    if (!Directory.Exists(pathToSave))
                    {
                        Directory.CreateDirectory(pathToSave);
                    }
                    await FileData.SaveFile(Path.Combine(pathToSave, entityExist.ImageUrl), request.ImageUrl);
                }
            }
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
