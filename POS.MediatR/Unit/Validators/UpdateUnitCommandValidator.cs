using FluentValidation;
using POS.MediatR.Unit.Commands;

namespace POS.MediatR.Unit.Validators
{
   public class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
    {
        public UpdateUnitCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Please enter unit name.");
        }
    }
}
