using MediatR;
using POS.Data.Resources;
using POS.Repository.BuzonSugerencias;

namespace POS.MediatR.BuzonSugerencias.Command
{
    public class GetAllBuzonSugerenciasCommand : IRequest<BuzonSugerenciasList>
    {
        public BuzonSugerenciasResource Resource { get; set; }
    }
}
