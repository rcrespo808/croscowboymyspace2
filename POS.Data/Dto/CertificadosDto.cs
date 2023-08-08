using System;

namespace POS.Data.Dto
{
    public class CertificadosDto
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Ruta { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid UserId { get; set; }
    }
}
