using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.Nosotros.Command;
using POS.Repository;
using POS.Repository.Eventos;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Eventos.Command
{
    public class GetAllEventosCommandHandler : IRequestHandler<GetAllEventosCommand, EventosList>
    {
        private readonly IEventosRepository _eventosRepository;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;

        public GetAllEventosCommandHandler(
            IEventosRepository eventosRepository,
            IPropertyMappingService propertyMappingService, 
            PathHelper pathHelper,
            IMapper mapper
            )
        {
            _eventosRepository = eventosRepository;
            _propertyMappingService = propertyMappingService;
            _pathHelper = pathHelper;
            _mapper = mapper;
        }

        public async Task<EventosList> Handle(GetAllEventosCommand request, CancellationToken cancellationToken)
        {
            var collectionBeforePaging = _eventosRepository.All.ApplySort(request.Resource.OrderBy,
                _propertyMappingService.GetPropertyMapping<EventosDto, Data.Entities.Lookups.Eventos>());

            if (request.Resource.Activo)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(e => DateOnly.FromDateTime(DateTime.Now) <= DateOnly.FromDateTime(e.FechaFinal));
            }

            if (request.Resource.Destacado.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Destacado == request.Resource.Destacado.Value);
            }

            if (!string.IsNullOrEmpty(request.Resource.Titulo))
            {
                // trim & ignore casing
                var genreForWhereClause = request.Resource.Titulo
                    .Trim().ToLowerInvariant();
                var name = Uri.UnescapeDataString(genreForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.Titulo, $"%{encodingName}%"));
            }

            collectionBeforePaging = collectionBeforePaging
                .Include(c => c.Panelistas)
                .ThenInclude(p => p.Customer);

            var eventosList = new EventosList(_mapper);
            return await eventosList.Create(collectionBeforePaging, request.Resource.Skip, request.Resource.PageSize);
        }
    }
}
