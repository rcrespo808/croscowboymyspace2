using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Helper;
using POS.MediatR.BeneficioCategoria.Command;
using POS.Repository;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Beneficios.Command
{
    public class GetAllBeneficiosCategoriasCommandHandler : IRequestHandler<GetAllBeneficiosCategoriasCommand, List<BeneficiosCategorias>>
    {
        private readonly IBeneficiosCategoriasRepository _beneficioCategoriaRepository;
        private readonly PathHelper _pathHelper;
        private readonly IPropertyMappingService _propertyMappingService;

        public GetAllBeneficiosCategoriasCommandHandler(
            IBeneficiosCategoriasRepository beneficioCategoriaRepository,
            PathHelper pathHelper,
            IPropertyMappingService propertyMappingService)
        {
            _beneficioCategoriaRepository = beneficioCategoriaRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<List<BeneficiosCategorias>> Handle(GetAllBeneficiosCategoriasCommand request, CancellationToken cancellationToken)
        {
            var beneficiosCategorias = _beneficioCategoriaRepository.All.AsNoTracking();

            if (request.IgnoreBeneficioCategoriaId.HasValue)
            {
                beneficiosCategorias = beneficiosCategorias.Where(b => b.Id != request.IgnoreBeneficioCategoriaId.Value);
            }

            beneficiosCategorias = beneficiosCategorias.Select(b => new BeneficiosCategorias
            {
                Id = b.Id,
                Nombre = b.Nombre,
                Descuento = b.Descuento,
                UrlBanner = !string.IsNullOrWhiteSpace(b.UrlBanner) ? Path.Combine(_pathHelper.BeneficioCategoriaImagePath, b.UrlBanner) : "",
            });

            return await beneficiosCategorias.ToListAsync();
        }
    }
}
