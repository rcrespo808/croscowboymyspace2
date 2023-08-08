using System;

namespace POS.Data
{
    public class Certificados : BaseEntity
    {
        public Guid Id { get; set; }
        
        public string Nombre { get; set; }

        public string Ruta { get; set; }

        public Guid UserId { get; set; }
    }
}
