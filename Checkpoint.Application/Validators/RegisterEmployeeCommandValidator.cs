using Checkpoint.Application.Commands.RegisterEmployee;
using Checkpoint.Shared.Utils;
using FluentValidation;

namespace Checkpoint.Application.Validators
{
    public partial class RegisterEmployeeCommandValidator
        : AbstractValidator<RegisterEmployeeCommand>
    {
        public RegisterEmployeeCommandValidator()
        {
            RuleFor(p => p.Email)
                .NotNull()
                .NotEmpty()
                .Must(Validations.IsValidEmail)
                .WithMessage("Please provide a valid email");

            RuleFor(p => p.Name).NotNull().NotEmpty().WithMessage("Please provide a valid name");

            RuleFor(p => p.User)
                .NotNull()
                .NotEmpty()
                .Must(Validations.IsValidUsername)
                .WithMessage(
                    "Please provide a valid username. (No _ or . at the end or at the beginning; No __ or _. or ._ or .. inside; 8 - 20 characters)."
                );

            RuleFor(p => p.Password)
                .NotNull()
                .NotEmpty()
                .Must(Validations.IsValidPassword)
                .WithMessage(
                    "Password must contain at least 8 characters, a number, an uppercase letter, a lowercase letter and a special character"
                );
        }
    }
}
