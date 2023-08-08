using POS.Helper.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data.Dto.Servicio
{
    public class ServicioExternoDto
    {
        public string Nombre { get; set; }

        public string Celular { get; set; }

        public string UrlImageData { get; set; }

        public string UrlWhatsapp { get; set; }

        public string UrlWeb { get; set; }

        public TipoPago TipoPago { get; set; }

        public decimal Costo { get; set; }

        public decimal CostoSocio { get; set; }

        public string Descripcion { get; set; }
    }
}
