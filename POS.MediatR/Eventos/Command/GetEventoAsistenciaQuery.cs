using MediatR;
using POS.Data;
using POS.Data.Dto;
using System;
using System.Collections.Generic;

namespace POS.MediatR.Eventos.Command
{
    public class GetEventoAsistenciaQuery : IRequest<List<UserAttendeeDto>>
    {
        public Guid Id { get; set; }

        public bool EstadoCuenta { get; set; }
    }
}
