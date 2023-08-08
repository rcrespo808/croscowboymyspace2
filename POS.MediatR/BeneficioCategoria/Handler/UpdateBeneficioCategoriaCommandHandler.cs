using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;
using POS.Helper;
using POS.MediatR.BeneficioCategoria.Command;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.BeneficioCategoria.Handler
{
    public class UpdateBeneficioCategoriaCommandHandler
      : IRequestHandler<UpdateBeneficioCategoriaCommand, ServiceResponse<BeneficiosCategorias>>
    {

        private readonly IBeneficiosCategoriasRepository _beneficioCategoriaRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<UpdateBeneficioCategoriaCommandHandler> _logger;

        public UpdateBeneficioCategoriaCommandHandler(
            IBeneficiosCategoriasRepository beneficioCategoriaRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            PathHelper pathHelper,
            IWebHostEnvironment webHostEnvironment,
            ILogger<UpdateBeneficioCategoriaCommandHandler> logger)
        {
            _beneficioCategoriaRepository = beneficioCategoriaRepository;
            _mapper = mapper;
            _uow = uow;
            _pathHelper = pathHelper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        public async Task<ServiceResponse<BeneficiosCategorias>> Handle(UpdateBeneficioCategoriaCommand request, CancellationToken cancellationToken)
        {
            var existingCategoria = await _beneficioCategoriaRepository.All.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (existingCategoria == null)
            {
                return ServiceResponse<BeneficiosCategorias>.Return404("El beneficioCategorias no existe");
            }

            var oldBannerUrl = existingCategoria.UrlBanner;
            existingCategoria = _mapper.Map(request, existingCategoria);
            _beneficioCategoriaRepository.Update(existingCategoria);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Hubo un error al momento de actualizar el beneficioCategoria");
                return ServiceResponse<BeneficiosCategorias>.Return500();
            }

            await FileManager.UpdateFile(request.IsBannerUpdated, _pathHelper.BeneficioCategoriaImagePath, existingCategoria.UrlBanner, request.UrlBannerData, oldBannerUrl);

            return ServiceResponse<BeneficiosCategorias>.ReturnResultWith201(existingCategoria);
        }
    }
}
