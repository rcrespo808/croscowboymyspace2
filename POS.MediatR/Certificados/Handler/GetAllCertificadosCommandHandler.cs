using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Helper;
using POS.Repository;
using POS.Repository.Certificados;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Certificados.Command
{
    public class GetAllCertificadosCommandHandler : IRequestHandler<GetAllCertificadosCommand, CertificadosList>
    {
        private readonly ICertificadosRepository _certificadosRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public GetAllCertificadosCommandHandler(
            ICertificadosRepository certificadosRepository,
            PathHelper pathHelper,
            IMapper mapper,
            IPropertyMappingService propertyMappingService)
        {
            _certificadosRepository = certificadosRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
            _mapper = mapper;
        }

        public async Task<CertificadosList> Handle(GetAllCertificadosCommand request, CancellationToken cancellationToken)
        {
            var collectionBeforePaging = _certificadosRepository.All;

            if (request.Resource.UserId.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.UserId == request.Resource.UserId.Value);
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

            var certificadosList = new CertificadosList(_mapper);
            return await certificadosList.Create(collectionBeforePaging, request.Resource.Skip, request.Resource.PageSize);
        }
    }
}
