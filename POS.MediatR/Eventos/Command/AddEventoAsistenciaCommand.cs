using MediatR;
using POS.Data;
using POS.Helper;
using System.Collections.Generic;
using System;

namespace POS.MediatR.Eventos.Command
{
    public class AddEventoAsistenciaCommand : IRequest<object>
    {
        public Guid? UsersId { get; set; }

        public Guid EventosId { get; set; }
    }
}
