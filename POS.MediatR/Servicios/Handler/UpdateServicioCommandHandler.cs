using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Servicios.Command;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Servicio.Handler
{
    public class UpdateServicioCommandHandler
      : IRequestHandler<UpdateServicioCommand, ServiceResponse<Data.Servicios>>
    {

        private readonly IServiciosRepository _servicioRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<UpdateServicioCommandHandler> _logger;

        public UpdateServicioCommandHandler(
            IServiciosRepository servicioRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            PathHelper pathHelper,
            IWebHostEnvironment webHostEnvironment,
            ILogger<UpdateServicioCommandHandler> logger)
        {
            _servicioRepository = servicioRepository;
            _mapper = mapper;
            _uow = uow;
            _pathHelper = pathHelper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        public async Task<ServiceResponse<Data.Servicios>> Handle(UpdateServicioCommand request, CancellationToken cancellationToken)
        {
            var existingServicio = await _servicioRepository.All.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (existingServicio == null)
            {
                return ServiceResponse<Data.Servicios>.Return404("El servicios no existe");
            }

            var oldImageUrl = existingServicio.UrlImage;
            existingServicio = _mapper.Map(request, existingServicio);
            _servicioRepository.Update(existingServicio);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Hubo un error al momento de actualizar el servicio");
                return ServiceResponse<Data.Servicios>.Return500();
            }

            await FileManager.UpdateFile(request.IsImageUpdated, _pathHelper.ServiciosImagePath, existingServicio.UrlImage, request.UrlImageData, oldImageUrl);

            existingServicio.UrlImage = FileManager.GetPathFile(existingServicio.UrlImage, _pathHelper.ServiciosImagePath);
            return ServiceResponse<Data.Servicios>.ReturnResultWith201(existingServicio);
        }
    }
}
