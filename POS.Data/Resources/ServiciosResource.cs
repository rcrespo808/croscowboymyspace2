using POS.Helper;
using POS.Helper.Enum;
using System;

namespace POS.Data
{
    public class ServiciosResource : ResourceParameters
    {
        public ServiciosResource() : base("")
        {

        }

        public string? Nombre { get; set; }
        public Guid? ServicioCategoriaId { get; set; }
    }
}
