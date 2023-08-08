using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Helper;
using POS.Repository;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Servicios.Command
{
    public class GetAllServiciosCommandHandler : IRequestHandler<GetAllServiciosCommand, ServiciosList>
    {
        private readonly IServiciosRepository _serviciosRepository;
        private readonly PathHelper _pathHelper;
        private readonly IPropertyMappingService _propertyMappingService;

        public GetAllServiciosCommandHandler(
            IServiciosRepository serviciosRepository,
            PathHelper pathHelper,
            IPropertyMappingService propertyMappingService)
        {
            _serviciosRepository = serviciosRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<ServiciosList> Handle(GetAllServiciosCommand request, CancellationToken cancellationToken)
        {
            var collectionBeforePaging = _serviciosRepository.All.Include(b => b.ServicioCategoria).AsNoTracking();

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

            if (request.Resource.ServicioCategoriaId.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.ServicioCategoriaId == request.Resource.ServicioCategoriaId.Value);
            }

            if (request.TipoServicio.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.TipoServicio == request.TipoServicio.Value);
            }

            var serviciosList = new ServiciosList(_pathHelper);
            return await serviciosList.Create(collectionBeforePaging, request.Resource.Skip, request.Resource.PageSize);
        }
    }
}
