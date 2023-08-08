using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data.Dto;
using POS.Data.Dto.InterestInformation;
using POS.Data.Entities.Lookups;
using POS.Data.Resources;
using POS.Helper;
using POS.Repository;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.InterestInformation.Command
{
    public class GetAllInterestInformationCommandHandler : IRequestHandler<GetAllInterestInformationCommand, InterestInformationList>
    {
        private readonly IInterestInformationRepository _interestInformationRepository;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IMapper _mapper;
        private readonly PathHelper _pathHelper;

        public GetAllInterestInformationCommandHandler(
            IInterestInformationRepository interestInformationRepository,
            IMapper mapper,
            IPropertyMappingService propertyMappingService, 
            PathHelper pathHelper)
        {
            _interestInformationRepository = interestInformationRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
            _mapper = mapper;
        }

        public async Task<InterestInformationList> Handle(GetAllInterestInformationCommand request, CancellationToken cancellationToken)
        {
            //var collectionBeforePaging = _interestInformationRepository.All;
            var collectionBeforePaging =
                _interestInformationRepository.All.ApplySort(request.InterestInformationResource.OrderBy,
                _propertyMappingService.GetPropertyMapping<InterestInformationDto, Informacioninteres>());

            if (!string.IsNullOrEmpty(request.InterestInformationResource.SearchQuery))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = request.InterestInformationResource.SearchQuery
                    .Trim().ToLowerInvariant();
                var name = Uri.UnescapeDataString(searchQueryForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");

                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.Titulo, $"{searchQueryForWhereClause}%")
                    || EF.Functions.Like(a.Descripcion, $"%{searchQueryForWhereClause}%")
                    || EF.Functions.Like(a.Contenido, $"%{searchQueryForWhereClause}%")
                    || EF.Functions.Like(a.Etiquetas, $"%{searchQueryForWhereClause}%")
                    );
            }

            if (!string.IsNullOrEmpty(request.InterestInformationResource.Tipo))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Tipo == request.InterestInformationResource.Tipo);
            }

            if (request.InterestInformationResource.Desde.HasValue && request.InterestInformationResource.Hasta.HasValue)
            {
                var desdeDate = request.InterestInformationResource.Desde.Value.Date;
                var hastaDate = request.InterestInformationResource.Hasta.Value.Date;
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => desdeDate <= a.Fecha.Date && a.Fecha.Date <= hastaDate);
            }

            var interestInformationList = new InterestInformationList(_mapper);
            return await interestInformationList.Create(collectionBeforePaging, request.InterestInformationResource.Skip, request.InterestInformationResource.PageSize);
        }
    }
}
