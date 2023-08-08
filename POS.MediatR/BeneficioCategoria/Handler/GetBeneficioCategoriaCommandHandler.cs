using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Helper;
using POS.MediatR.BeneficioCategoria.Command;
using POS.Repository;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Beneficios.Command
{
    public class GetBeneficioCategoriaCommandHandler : IRequestHandler<GetBeneficioCategoriaCommand, BeneficiosCategorias>
    {
        private readonly IBeneficiosCategoriasRepository _beneficioCategoriaRepository;
        private readonly PathHelper _pathHelper;
        private readonly IPropertyMappingService _propertyMappingService;

        public GetBeneficioCategoriaCommandHandler(
            IBeneficiosCategoriasRepository beneficioCategoriaRepository,
            PathHelper pathHelper,
            IPropertyMappingService propertyMappingService)
        {
            _beneficioCategoriaRepository = beneficioCategoriaRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<BeneficiosCategorias> Handle(GetBeneficioCategoriaCommand request, CancellationToken cancellationToken)
        {
            var beneficioCategoria = await _beneficioCategoriaRepository
                .All
                .Where(b => b.Id == request.Id)
                .Select(b => (new BeneficiosCategorias
                {
                    Id = b.Id,
                    Nombre = b.Nombre,
                    Descuento = b.Descuento,
                    UrlBanner = !string.IsNullOrWhiteSpace(b.UrlBanner) ? Path.Combine(_pathHelper.BeneficioCategoriaImagePath, b.UrlBanner) : "",
                })
                ).FirstOrDefaultAsync();
            return beneficioCategoria;
        }
    }
}
