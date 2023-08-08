using POS.Helper;
using POS.Helper.Enum.BuzonSugerencias;

namespace POS.Data.Resources
{
    public class BuzonSugerenciasResource : ResourceParameters
    {
        public BuzonSugerenciasResource() : base("CreatedDate")
        {

        }
        public string Nombre { get; set; }

        public EstadoSugerencia? Estado { get; set; }

        public TipoSugerencia? Tipo { get; set; }
    }
}
