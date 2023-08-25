using Checkpoint.Core.Models.InputModels;
using FluentValidation;

namespace Checkpoint.Application.Validators
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailInputModel>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(p => p.ConfirmationCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please, provide a value for confirmation code");
        }
    }
}
