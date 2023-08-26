using System.Net;
using System.Text.Json;
using Checkpoint.API.Attributes;
using Checkpoint.Application.Queries.GetEmployeeEmailAlreadyVerified;
using Checkpoint.Core.DomainServices.Auth;
using Checkpoint.Core.Models.ViewModels;
using MediatR;

namespace Checkpoint.API.Middlewares
{
    public class EmailVerificationMiddleware : IMiddleware
    {
        private readonly IAuthDomainService _authDomainService;
        private readonly IMediator _mediator;

        public EmailVerificationMiddleware(IAuthDomainService authDomainService, IMediator mediator)
        {
            _authDomainService = authDomainService;
            _mediator = mediator;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var endpoint = context.GetEndpoint();

            bool isEmployeeAuthenticated = context.User.Identity?.IsAuthenticated ?? false;

            if (endpoint != null && isEmployeeAuthenticated)
            {
                var requiresConfirmedEmailAttribute =
                    endpoint.Metadata.GetMetadata<RequiresConfirmedEmail>();

                if (requiresConfirmedEmailAttribute != null)
                {
                    var employeeClaims = _authDomainService.ReadUserClaims(context.User.Claims);

                    var query = new GetEmployeeEmailAlreadyVerifiedQuery(employeeClaims.Email);

                    var employeeEmailIsConfirmed = await _mediator.Send(query);

                    if (!employeeEmailIsConfirmed)
                    {
                        context.Response.ContentType = "application/json";

                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;

                        var responseData = new DefaultResponseViewModel(
                            "Confirm your email to access this feature!"
                        );

                        var json = JsonSerializer.Serialize(responseData);

                        await context.Response.WriteAsync(json);

                        return;
                    }
                }
            }

            await next(context);
        }
    }
}
