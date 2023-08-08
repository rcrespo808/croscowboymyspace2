using MediatR;
using POS.Data;
using POS.Helper;
using POS.Repository;

namespace POS.MediatR.Publicidad.Command
{
    public class GetAllPublicidadCommand : IRequest<PublicidadList>
    {
        public PublicidadResource Resource { get; set; }
    }
}
