using Checkpoint.Core.Enums;
using Checkpoint.Core.Models.InputModels;
using FluentValidation;

namespace Checkpoint.Application.Validators
{
    public class ClockInCommandValidator : AbstractValidator<ClockInInputModel>
    {
        public ClockInCommandValidator()
        {
            RuleFor(p => p.CheckpointType)
                .NotNull()
                .NotEmpty()
                .Must(BeAValidOption)
                .WithMessage("Enter a valid checkpoint type (A for Arrival or E for Exit).");
        }

        public static bool BeAValidOption(char checkpointType)
        {
            List<char> validOptions =
                new() { (char)PointLogTypeEnum.Arrival, (char)PointLogTypeEnum.Exit };

            return validOptions.Contains(checkpointType);
        }
    }
}
