using MediatR;
using POS.Data;
using POS.Helper;
using POS.Helper.Enum.EstadoCuenta;
using System;

namespace POS.MediatR.EstadoCuenta.Command
{
    public class AddEstadoCuentaCommand : IRequest<ServiceResponse<EstadoCuentaDto>>
    {
        public string Nombre { get; set; }

        public DateTime FechaCobro { get; set; }

        public DateTime FechaPago { get; set; }

        public TipoEstado Estado { get; set; }

        public double Monto { get; set; }

        public Guid IdUsuario { get; set; }

        public Guid IdFactura { get; set; }
    }
}
