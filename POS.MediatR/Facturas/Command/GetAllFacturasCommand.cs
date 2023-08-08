using MediatR;
using POS.Data.Resources;
using POS.Repository.Facturas;

namespace POS.MediatR.Facturas.Command
{
    public class GetAllFacturasCommand : IRequest<FacturasList>
    {
        public FacturasResource Resource { get; set; }
    }
}
