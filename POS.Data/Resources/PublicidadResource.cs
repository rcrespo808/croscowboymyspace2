using POS.Helper;
using System;

namespace POS.Data
{
    public class PublicidadResource : ResourceParameters
    {
        public PublicidadResource() : base("CreatedDate")
        {

        }
        public string? Nombre { get; set; }
    }
}
