using POS.Helper;
using System;

namespace POS.Data.Resources
{
    public class CertificadosResource : ResourceParameters
    {
        public CertificadosResource() : base("CreatedDate")
        {

        }
        public string Nombre { get; set; }

        public Guid? UserId { get; set; }
    }
}
