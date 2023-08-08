using POS.Helper.Enum.EstadoCuenta;
using System;

namespace POS.Data
{
    public class EstadoCuenta : BaseEntity
    {
        public Guid Id { get; set; }
        
        public string Nombre { get; set; }

        public DateTime? FechaCobro { get; set; }

        public DateTime? FechaPago { get; set; }

        public TipoEstado Estado { get; set; }

        public double? Monto { get; set; }

        public Guid? IdUsuario { get; set; }

        public Guid? IdFactura { get; set; }

        public Facturas? Factura { get; set; }
    }
}
