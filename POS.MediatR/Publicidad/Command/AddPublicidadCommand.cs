using MediatR;
using POS.Data.Dto;
using POS.Helper;

namespace POS.MediatR.Publicidad.Command
{
    public class AddPublicidadCommand : IRequest<ServiceResponse<PublicidadDto>>
    {
        public string Nombre { get; set; }
        
        public string Link { get; set; }

        public string UrlBannerData { get; set; }
    }
}
