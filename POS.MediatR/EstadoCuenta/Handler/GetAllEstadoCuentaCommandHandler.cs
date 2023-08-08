using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Helper;
using POS.Repository;
using POS.Repository.EstadoCuenta;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.EstadoCuenta.Command
{
    public class GetAllEstadoCuentaCommandHandler : IRequestHandler<GetAllEstadoCuentaCommand, EstadoCuentaList>
    {
        private readonly IEstadoCuentaRepository _estadoCuentaRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public GetAllEstadoCuentaCommandHandler(
            IEstadoCuentaRepository estadoCuentaRepository,
            PathHelper pathHelper,
            IMapper mapper,
            IPropertyMappingService propertyMappingService)
        {
            _estadoCuentaRepository = estadoCuentaRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
            _mapper = mapper;
        }

        public async Task<EstadoCuentaList> Handle(GetAllEstadoCuentaCommand request, CancellationToken cancellationToken)
        {
            var collectionBeforePaging = _estadoCuentaRepository.All;

            if (request.Resource.Estado.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Estado == request.Resource.Estado.Value);
            }

            if (request.Resource.IdUsuario.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.IdUsuario == request.Resource.IdUsuario.Value);
            }

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

            var estadoCuentaList = new EstadoCuentaList(_mapper);
            return await estadoCuentaList.Create(collectionBeforePaging, request.Resource.Skip, request.Resource.PageSize);
        }
    }
}
