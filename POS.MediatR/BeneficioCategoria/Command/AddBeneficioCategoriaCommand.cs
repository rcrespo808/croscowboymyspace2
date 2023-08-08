using MediatR;
using POS.Data;
using POS.Helper;
using System;

namespace POS.MediatR.BeneficioCategoria.Command
{
    public class AddBeneficioCategoriaCommand : IRequest<ServiceResponse<BeneficiosCategorias>>
    {
        public string Nombre { get; set; }

        public bool Descuento { get; set; }

        public string UrlBannerData { get; set; }
    }
}
