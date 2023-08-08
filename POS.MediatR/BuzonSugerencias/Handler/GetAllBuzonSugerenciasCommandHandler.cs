using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Helper;
using POS.Repository;
using POS.Repository.BuzonSugerencias;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.BuzonSugerencias.Command
{
    public class GetAllBuzonSugerenciasCommandHandler : IRequestHandler<GetAllBuzonSugerenciasCommand, BuzonSugerenciasList>
    {
        private readonly IBuzonSugerenciasRepository _buzonSugerenciasRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public GetAllBuzonSugerenciasCommandHandler(
            IBuzonSugerenciasRepository buzonSugerenciasRepository,
            PathHelper pathHelper,
            IMapper mapper,
            IPropertyMappingService propertyMappingService)
        {
            _buzonSugerenciasRepository = buzonSugerenciasRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
            _mapper = mapper;
        }

        public async Task<BuzonSugerenciasList> Handle(GetAllBuzonSugerenciasCommand request, CancellationToken cancellationToken)
        {
            var collectionBeforePaging = _buzonSugerenciasRepository.All;

            if (request.Resource.Tipo.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Tipo == request.Resource.Tipo.Value);
            }

            if (request.Resource.Estado.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Estado == request.Resource.Estado.Value);
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

            var buzonSugerenciasList = new BuzonSugerenciasList(_mapper);
            return await buzonSugerenciasList.Create(collectionBeforePaging, request.Resource.Skip, request.Resource.PageSize);
        }
    }
}
