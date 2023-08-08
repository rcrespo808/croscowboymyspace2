using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.Eventos.Command;
using POS.Repository;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Eventos.Handler
{
    public class GetEventoAsistenciaQueryHandler : IRequestHandler<GetEventoAsistenciaQuery, List<UserAttendeeDto>>
    {
        private readonly IEventosRepository _eventosRepository;
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;

        public GetEventoAsistenciaQueryHandler(
            IEventosRepository eventosRepository,
            IAsistenciaRepository asistenciaRepository,
            PathHelper pathHelper,
            IMapper mapper)
        {
            _eventosRepository = eventosRepository;
            _asistenciaRepository = asistenciaRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
        }
        public async Task<List<UserAttendeeDto>> Handle(GetEventoAsistenciaQuery request, CancellationToken cancellationToken)
        {
            List<UserAttendeeDto> asistencia;

            if (request.EstadoCuenta) {
                asistencia = await _asistenciaRepository
                                                .All
                                                .Include(i => i.User)
                                                .Where(a => a.EventosId == request.Id)
                                                .ProjectTo<UserAttendeeDto>(_mapper.ConfigurationProvider)
                                                .ToListAsync();
            }
            else
            {

                asistencia = await _asistenciaRepository
                                                .All
                                                .Include(i => i.User)
                                                .Where(a => a.EventosId == request.Id)
                                                .Select(e => new UserAttendeeDto {
                                                    Id = e.User.Id,
                                                    UserName = e.User.UserName,
                                                    Email = e.User.Email,
                                                    FirstName = e.User.FirstName,
                                                    LastName = e.User.LastName,
                                                    PhoneNumber = e.User.PhoneNumber,
                                                    Address = e.User.Address,
                                                    ProfilePhoto = string.IsNullOrWhiteSpace(e.User.ProfilePhoto) ? "" : Path.Combine(_pathHelper.UserProfilePath, e.User.ProfilePhoto),
                                                    EstadoCuenta = null
                                                 })
                                                .ToListAsync();
            }

            return asistencia;
        }
    }
}
