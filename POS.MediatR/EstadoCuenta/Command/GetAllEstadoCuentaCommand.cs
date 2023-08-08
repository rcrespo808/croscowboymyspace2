using MediatR;
using POS.Data.Resources;
using POS.Repository.EstadoCuenta;

namespace POS.MediatR.EstadoCuenta.Command
{
    public class GetAllEstadoCuentaCommand : IRequest<EstadoCuentaList>
    {
        public EstadoCuentaResource Resource { get; set; }
    }
}
