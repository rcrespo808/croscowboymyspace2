using MediatR;
using POS.Data;
using POS.Helper;
using System;

namespace POS.MediatR.BeneficioCategoria.Command
{
    public class UpdateBeneficioCategoriaCommand : IRequest<ServiceResponse<BeneficiosCategorias>>
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public bool Descuento { get; set; }

        public string UrlBannerData { get; set; }
        
        public bool IsBannerUpdated { get; set; }
    }
}
