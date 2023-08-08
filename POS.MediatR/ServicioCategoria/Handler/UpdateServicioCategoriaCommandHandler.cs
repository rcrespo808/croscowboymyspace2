using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;
using POS.Helper;
using POS.MediatR.ServicioCategoria.Command;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.ServicioCategoria.Handler
{
    public class UpdateServicioCategoriaCommandHandler
      : IRequestHandler<UpdateServicioCategoriaCommand, ServiceResponse<ServiciosCategorias>>
    {

        private readonly IServiciosCategoriasRepository _beneficioCategoriaRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<UpdateServicioCategoriaCommandHandler> _logger;

        public UpdateServicioCategoriaCommandHandler(
            IServiciosCategoriasRepository beneficioCategoriaRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            PathHelper pathHelper,
            IWebHostEnvironment webHostEnvironment,
            ILogger<UpdateServicioCategoriaCommandHandler> logger)
        {
            _beneficioCategoriaRepository = beneficioCategoriaRepository;
            _mapper = mapper;
            _uow = uow;
            _pathHelper = pathHelper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        public async Task<ServiceResponse<ServiciosCategorias>> Handle(UpdateServicioCategoriaCommand request, CancellationToken cancellationToken)
        {
            var existingCategoria = await _beneficioCategoriaRepository.All.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (existingCategoria == null)
            {
                return ServiceResponse<ServiciosCategorias>.Return404("El beneficioCategorias no existe");
            }

            var oldImageUrl = existingCategoria.UrlImage;
            existingCategoria = _mapper.Map(request, existingCategoria);
            _beneficioCategoriaRepository.Update(existingCategoria);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Hubo un error al momento de actualizar el beneficioCategoria");
                return ServiceResponse<ServiciosCategorias>.Return500();
            }

            await FileManager.UpdateFile(request.IsImageUpdated, _pathHelper.ServicioCategoriaImagePath, existingCategoria.UrlImage, request.UrlImageData, oldImageUrl);
            existingCategoria.UrlImage = FileManager.GetPathFile(existingCategoria.UrlImage, _pathHelper.ServiciosImagePath);
            return ServiceResponse<ServiciosCategorias>.ReturnResultWith201(existingCategoria);
        }
    }
}
