using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data.Resources;
using POS.Helper;
using POS.Repository;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Beneficios.Command
{
    public class GetAllBeneficiosCommandHandler : IRequestHandler<GetAllBeneficiosCommand, BeneficiosList>
    {
        private readonly IBeneficiosRepository _beneficiosRepository;
        private readonly PathHelper _pathHelper;
        private readonly IPropertyMappingService _propertyMappingService;

        public GetAllBeneficiosCommandHandler(
            IBeneficiosRepository beneficiosRepository,
            PathHelper pathHelper,
            IPropertyMappingService propertyMappingService)
        {
            _beneficiosRepository = beneficiosRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<BeneficiosList> Handle(GetAllBeneficiosCommand request, CancellationToken cancellationToken)
        {
            var collectionBeforePaging = _beneficiosRepository.All.Include(b => b.BeneficioCategoria).AsNoTracking();

            if (!string.IsNullOrEmpty(request.Resource.Nombre))
            {
                // trim & ignore casing
                var genreForWhereClause = request.Resource.Nombre
                    .Trim().ToLowerInvariant();
                var name = Uri.UnescapeDataString(genreForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.Nombre, $"%{encodingName}%"));
            }

            if (request.Resource.BeneficioCategoriaId.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.BeneficioCategoriaId == request.Resource.BeneficioCategoriaId.Value);
            }

            if (request.IgnoreBeneficioCategoriaId.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.BeneficioCategoriaId != request.IgnoreBeneficioCategoriaId.Value);
            }

            var beneficiosList = new BeneficiosList();
            return await beneficiosList.Create(collectionBeforePaging, request.Resource.Skip, request.Resource.PageSize);
        }
    }
}
