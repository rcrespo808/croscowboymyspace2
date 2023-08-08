using POS.Helper;
using POS.Helper.Enum.EstadoCuenta;
using System;

namespace POS.Data.Resources
{
    public class EstadoCuentaResource : ResourceParameters
    {
        public EstadoCuentaResource() : base("CreatedDate")
        {

        }
        public string Nombre { get; set; }

        public TipoEstado? Estado { get; set; }

        public Guid? IdUsuario { get; set; }
    }
}
