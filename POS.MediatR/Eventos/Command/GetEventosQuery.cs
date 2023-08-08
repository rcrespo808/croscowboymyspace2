using MediatR;
using POS.Data.Dto;
using System;

namespace POS.MediatR.Eventos.Command
{
    public class GetEventosQuery : IRequest<EventosDto>
    {
        public Guid Id { get; set; }
    }
}
