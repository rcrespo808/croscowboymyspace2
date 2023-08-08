using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Helper;
using POS.Repository;
using POS.Repository.Facturas;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Facturas.Command
{
    public class GetAllFacturasCommandHandler : IRequestHandler<GetAllFacturasCommand, FacturasList>
    {
        private readonly IFacturasRepository _facturasRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public GetAllFacturasCommandHandler(
            IFacturasRepository facturasRepository,
            PathHelper pathHelper,
            IMapper mapper,
            IPropertyMappingService propertyMappingService)
        {
            _facturasRepository = facturasRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
            _mapper = mapper;
        }

        public async Task<FacturasList> Handle(GetAllFacturasCommand request, CancellationToken cancellationToken)
        {
            var collectionBeforePaging = _facturasRepository.All;

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

            var facturasList = new FacturasList(_mapper);
            return await facturasList.Create(collectionBeforePaging, request.Resource.Skip, request.Resource.PageSize);
        }
    }
}
