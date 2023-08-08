using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Data
{
    public class Facturas : BaseEntity
    {
        public Guid Id { get; set; }
        
        public string Nombre { get; set; }

        public DateTime? FechaPago { get; set; }

        public string UrlArchivo { get; set; }
        
        public Guid? IdUsuario { get; set; }

        public EstadoCuenta? EstadoCuenta { get; set; }
    }
}
