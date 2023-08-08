using POS.Helper;
using System;

namespace POS.Data
{
    public class EmpresasResource : ResourceParameters
    {
        public EmpresasResource() : base("CreatedDate")
        {

        }
        public string? Nombre { get; set; }
    }
}
