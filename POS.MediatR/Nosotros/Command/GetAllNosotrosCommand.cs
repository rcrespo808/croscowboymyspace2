using MediatR;
using POS.Helper;
using POS.Repository;

namespace POS.MediatR.Nosotros.Command
{
    public class GetAllNosotrosCommand : IRequest<NosotrosList>
    {
        public ResourceParameters Resource { get; set; }
    }
}
