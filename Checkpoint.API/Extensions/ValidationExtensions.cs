using Checkpoint.API.Filters;
using Checkpoint.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Checkpoint.API.Extensions
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidations(this IServiceCollection services)
        {
            services
                .AddControllers(o => o.Filters.Add(typeof(ValidationFilter)))
                .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);

            services.AddFluentValidationAutoValidation();

            services.AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<RegisterEmployeeCommandValidator>();

            return services;
        }
    }
}
