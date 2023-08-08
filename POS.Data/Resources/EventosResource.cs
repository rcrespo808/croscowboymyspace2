using POS.Helper;

namespace POS.Data.Resources
{
    public class EventosResource : ResourceParameters
    {
        public EventosResource() : base("Titulo")
        {
        }

        public string Titulo { get; set; }

        public bool Activo { get; set; } = false;

        public bool? Destacado { get; set; }
    }
}
