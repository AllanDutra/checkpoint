using System.Net.Mail;
using System.Text.RegularExpressions;
using Checkpoint.Application.Commands.RegisterEmployee;
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
                .Must(IsValidEmail)
                .WithMessage("Please provide a valid email");

            RuleFor(p => p.Name).NotNull().NotEmpty().WithMessage("Please provide a valid name");

            RuleFor(p => p.User)
                .NotNull()
                .NotEmpty()
                .Must(IsValidUsername)
                .WithMessage(
                    "Please provide a valid username. (No _ or . at the end or at the beginning; No __ or _. or ._ or .. inside; 8 - 20 characters)."
                );

            RuleFor(p => p.Password)
                .NotNull()
                .NotEmpty()
                .Must(IsValidPassword)
                .WithMessage(
                    "Password must contain at least 8 characters, a number, an uppercase letter, a lowercase letter and a special character"
                );
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mail = new(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsValidUsername(string user)
        {
            Regex usernameRegex = UsernameRegex();

            return usernameRegex.IsMatch(user);
        }

        public static bool IsValidPassword(string password)
        {
            Regex passwordRegex = PasswordRegex();

            return passwordRegex.IsMatch(password);
        }

        [GeneratedRegex("^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$")]
        private static partial Regex UsernameRegex();

        [GeneratedRegex("^.*(?=.{8,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$")]
        private static partial Regex PasswordRegex();
    }
}
