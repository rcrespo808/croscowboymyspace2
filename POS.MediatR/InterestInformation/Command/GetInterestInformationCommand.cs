using MediatR;
using POS.Data.Dto.InterestInformation;
using POS.Data.Entities.Lookups;
using POS.Helper;
using System;

namespace POS.MediatR.InterestInformation.Command
{
    public class GetInterestInformationCommand : IRequest<ServiceResponse<InterestInformationDto>>
    {
        public Guid Id { get; set; }
    }
}
