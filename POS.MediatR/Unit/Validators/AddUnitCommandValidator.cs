using FluentValidation;
using POS.MediatR.Unit.Commands;

namespace POS.MediatR.Unit.Validators
{
   public class AddUnitCommandValidator : AbstractValidator<AddUnitCommand>
    {
        public AddUnitCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Please enter unit name.");
        }
    }
}
