using MediatR;
using POS.Data.Dto;
using POS.Helper;
using System;

namespace POS.MediatR.Unit.Commands
{
   public class GetUnitCommand: IRequest<ServiceResponse<UnitDto>>
    {
        public Guid Id { get; set; }
    }
}
