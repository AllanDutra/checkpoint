using Checkpoint.Core.Enums;
using Checkpoint.Core.Models.InputModels;
using FluentValidation;

namespace Checkpoint.Application.Validators
{
#nullable enable
    public class GetInfoFromOtherEmployeesQueryValidator
        : AbstractValidator<GetInfoFromOtherEmployeesInputModel>
    {
        public GetInfoFromOtherEmployeesQueryValidator()
        {
            RuleFor(p => p.Filter)
                .Must(BeAValidFilter)
                .WithMessage("Provide a valid filter, options: 'Available' or 'Unavailable'.");

            RuleFor(p => p.Ordination)
                .Must(BeAValidOrdination)
                .WithMessage(
                    "Provide a valid ordination, being 'ASC' for ascending or 'DESC' for descending."
                );
        }

        public static bool BeAValidFilter(string? filter)
        {
            if (filter == null)
                return true;

            List<string> validFilters =
                new() { EmployeesFilterEnum.AVAILABLE, EmployeesFilterEnum.UNAVAILABLE };

            return validFilters.Contains(filter.ToUpper());
        }

        public static bool BeAValidOrdination(string? ordination)
        {
            if (ordination == null)
                return true;

            List<string> validOrdinations = new() { OrdinationEnum.ASC, OrdinationEnum.DESC };

            return validOrdinations.Contains(ordination.ToUpper());
        }
    }
}
