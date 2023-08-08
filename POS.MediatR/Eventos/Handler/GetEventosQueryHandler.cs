using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data.Dto;
using POS.MediatR.Eventos.Command;
using POS.Repository;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Eventos.Handler
{
    public class GetEventosQueryHandler : IRequestHandler<GetEventosQuery, EventosDto>
    {
        private readonly IEventosRepository _eventosRepository;
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfoToken;

        public GetEventosQueryHandler(
            IAsistenciaRepository asistenciaRepository,
            IEventosRepository eventosRepository,
            IMapper mapper,
            UserInfoToken userInfoToken)
        {
            _asistenciaRepository = asistenciaRepository;
            _eventosRepository = eventosRepository;
            _mapper = mapper;
            _userInfoToken = userInfoToken;
        }
        public async Task<EventosDto> Handle(GetEventosQuery request, CancellationToken cancellationToken)
        {
            var eventos = await _eventosRepository
                                                .FindBy(eventos => eventos.Id == request.Id)
                                                .Include(e => e.Panelistas)
                                                .ThenInclude(e => e.Customer)
                                                .FirstOrDefaultAsync();

            var result = _mapper.Map<Data.Entities.Lookups.Eventos, EventosDto>(eventos);

            if (_asistenciaRepository.All.Any(a => a.UsersId == Guid.Parse(_userInfoToken.Id) && a.EventosId == eventos.Id))
            {
                result.IsRegistered = true;
            }

            return result;
        }
    }
}
