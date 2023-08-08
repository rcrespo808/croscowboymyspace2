using POS.Helper;

namespace POS.Data.Resources
{
    public class FacturasResource : ResourceParameters
    {
        public FacturasResource() : base("CreatedDate")
        {

        }
        public string Nombre { get; set; }
    }
}
