using System;

namespace POS.Data
{
    public class FacturasDto
    {
        public Guid Id { get; set; }
        
        public string Nombre { get; set; }

        public DateTime? FechaPago { get; set; }

        public string UrlArchivo { get; set; }
        
        public Guid? IdUsuario { get; set; }
    }
}
