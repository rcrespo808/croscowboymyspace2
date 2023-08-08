using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class AddBeneficioCategoriaCommandHandler
        : IRequestHandler<AddBeneficioCategoriaCommand, ServiceResponse<BeneficiosCategorias>>
    {

        private readonly IBeneficiosCategoriasRepository _beneficioCategoriaRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<POSDbContext> _uow;

        public AddBeneficioCategoriaCommandHandler(IBeneficiosCategoriasRepository beneficioCategoriaRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow)
        {
            _beneficioCategoriaRepository = beneficioCategoriaRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
        }
        public async Task<ServiceResponse<BeneficiosCategorias>> Handle(AddBeneficioCategoriaCommand request, CancellationToken cancellationToken)
        {
            var existingBeneficioCategoria = await _beneficioCategoriaRepository.All
                .FirstOrDefaultAsync(c => c.Nombre == request.Nombre);
            if (existingBeneficioCategoria != null)
            {
                return ServiceResponse<BeneficiosCategorias>.Return409("Existe un Beneficio Categoria con el mismo nombre");
            }

            var beneficioCategoria = _mapper.Map<BeneficiosCategorias>(request);
            _beneficioCategoriaRepository.Add(beneficioCategoria);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<BeneficiosCategorias>.Return500();
            }

            await FileManager.SaveFile(beneficioCategoria.UrlBanner, request.UrlBannerData, _pathHelper.BeneficioCategoriaImagePath);
            beneficioCategoria.UrlBanner = FileManager.GetPathFile(beneficioCategoria.UrlBanner, _pathHelper.BeneficioCategoriaImagePath);
            return ServiceResponse<BeneficiosCategorias>.ReturnResultWith201(beneficioCategoria);
        }
    }
}
