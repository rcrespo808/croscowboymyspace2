using MediatR;
using POS.Helper;
using POS.Helper.Enum.EstadoCuenta;
using System;

namespace POS.MediatR.EstadoCuenta.Commands
{
    public class UpdateEstadoCuentaCommand : IRequest<ServiceResponse<Data.EstadoCuentaDto>>
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaCobro { get; set; }

        public DateTime FechaPago { get; set; }

        public TipoEstado Estado { get; set; }

        public double Monto { get; set; }

        public Guid IdUsuario { get; set; }

        public Guid IdFactura { get; set; }
    }
}
