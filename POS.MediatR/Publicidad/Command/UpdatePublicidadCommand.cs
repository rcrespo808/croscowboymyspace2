using MediatR;
using POS.Data.Dto;
using POS.Helper;
using System;

namespace POS.MediatR.Publicidad.Commands
{
    public class UpdatePublicidadCommand : IRequest<ServiceResponse<PublicidadDto>>
    {
        public Guid Id { get; set; }
        
        public string Nombre { get; set; }

        public string Link { get; set; }

        public string UrlBannerData { get; set; }

        public bool IsBannerChanged { get; set; }
    }
}
