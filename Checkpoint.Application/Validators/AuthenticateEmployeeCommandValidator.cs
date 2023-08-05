using Checkpoint.Application.Commands.AuthenticateEmployee;
using Checkpoint.Shared.Utils;
using FluentValidation;

namespace Checkpoint.Application.Validators
{
    public class AuthenticateEmployeeCommandValidator
        : AbstractValidator<AuthenticateEmployeeCommand>
    {
        public AuthenticateEmployeeCommandValidator()
        {
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
                .WithMessage("Please provide a valid password.");
        }
    }
}
