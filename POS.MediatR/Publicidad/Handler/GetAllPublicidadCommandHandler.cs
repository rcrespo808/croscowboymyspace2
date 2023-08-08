using AutoMapper;
using MediatR;
using POS.Helper;
using POS.Repository;
using System.Net;
using System.Text.RegularExpressions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace POS.MediatR.Publicidad.Command
{
    public class GetAllPublicidadCommandHandler : IRequestHandler<GetAllPublicidadCommand, PublicidadList>
    {
        private readonly IPublicidadRepository _publicidadRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public GetAllPublicidadCommandHandler(
            IPublicidadRepository publicidadRepository,
            PathHelper pathHelper,
            IMapper mapper,
            IPropertyMappingService propertyMappingService)
        {
            _publicidadRepository = publicidadRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
            _mapper = mapper;
        }

        public async Task<PublicidadList> Handle(GetAllPublicidadCommand request, CancellationToken cancellationToken)
        {
            var collectionBeforePaging = _publicidadRepository.All;
            
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

            var publicidadList = new PublicidadList(_mapper);
            return await publicidadList.Create(collectionBeforePaging, request.Resource.Skip, request.Resource.PageSize);
        }
    }
}
