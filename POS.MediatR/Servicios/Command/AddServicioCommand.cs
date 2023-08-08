using MediatR;
using POS.Helper;
using POS.Helper.Enum;
using System;

namespace POS.MediatR.Servicios.Command
{
    public class AddServicioCommand : IRequest<ServiceResponse<Data.Servicios>>
    {
        public TipoServicio TipoServicio { get; set; }

        public string Nombre { get; set; }

        public string Celular { get; set; }

        public string UrlImageData { get; set; }

        public string UrlWhatsapp { get; set; }

        public string UrlWeb { get; set; }

        public TipoPago TipoPago { get; set; }

        public decimal Costo { get; set; }

        public decimal CostoSocio { get; set; }

        public string Descripcion { get; set; }

        public Guid ServicioCategoriaId { get; set; }
    }
}
