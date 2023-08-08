using MediatR;
using POS.Data.Resources;
using POS.Helper;
using POS.Repository;

namespace POS.MediatR.InterestInformation.Command
{
    public class GetAllInterestInformationCommand : IRequest<InterestInformationList>
    {
        public InterestInformationResource InterestInformationResource { get; set; }
    }
}
