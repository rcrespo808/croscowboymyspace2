using MediatR;
using POS.Helper;
using System;

namespace POS.MediatR.Unit.Commands
{
   public class UpdateUnitCommand: IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
