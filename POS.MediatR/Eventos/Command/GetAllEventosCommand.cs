using MediatR;
using POS.Data.Resources;
using POS.Repository;
using POS.Repository.Eventos;

namespace POS.MediatR.Nosotros.Command
{
    public class GetAllEventosCommand : IRequest<EventosList>
    {
        public EventosResource Resource { get; set; }
    }
}
