using POS.Helper;
using System;

namespace POS.Data
{
    public class BeneficiosResource : ResourceParameters
    {
        public BeneficiosResource() : base("")
        {

        }
        public string? Nombre { get; set; }
        public Guid? BeneficioCategoriaId { get; set; }

    }
}
