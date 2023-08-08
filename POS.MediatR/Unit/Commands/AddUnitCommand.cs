using MediatR;
using POS.Data.Dto;
using POS.Helper;

namespace POS.MediatR.Unit.Commands
{
   public class AddUnitCommand: IRequest<ServiceResponse<UnitDto>>
    {
        public string Name { get; set; }
    }
}
