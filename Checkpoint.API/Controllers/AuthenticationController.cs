using Checkpoint.Application.Commands.AuthenticateEmployee;
using Checkpoint.Application.Commands.RegisterEmployee;
using Checkpoint.Core.DomainServices.Auth;
using Checkpoint.Core.Interfaces.Notifications;
using Checkpoint.Core.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Checkpoint.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : MainController
    {
        public AuthenticationController(
            IMediator mediator,
            INotifier notifier,
            IAuthDomainService authDomainService
        )
            : base(mediator, notifier, authDomainService) { }

        /// <summary>
        /// Register a new employee in database
        /// </summary>
        /// <param name="command"></param>
        [AllowAnonymous]
        [HttpPost("register-employee")]
        [ProducesResponseType(typeof(DefaultResponseViewModel), 200)]
        [ProducesResponseType(typeof(DefaultResponseViewModel), 400)]
        public async Task<IActionResult> RegisterEmployeeAsync(
            [FromBody] RegisterEmployeeCommand command
        )
        {
            await _mediator.Send(command);

            return PersonalizedResponse(
                Ok(
                    new DefaultResponseViewModel(
                        "New employee successfully registered! The next step is to confirm your email."
                    )
                )
            );
        }

        /// <summary>
        /// Generate a jwt token for authenticate employee
        /// </summary>
        /// <param name="command"></param>
        [AllowAnonymous]
        [HttpPost("authenticate-employee")]
        [ProducesResponseType(typeof(EmployeeAuthenticationViewModel), 200)]
        [ProducesResponseType(typeof(DefaultResponseViewModel), 404)]
        public async Task<IActionResult> AuthenticateEmployeeAsync(
            [FromBody] AuthenticateEmployeeCommand command
        )
        {
            var employeeAuthentication = await _mediator.Send(command);

            return PersonalizedResponse(Ok(employeeAuthentication));
        }

        /// <summary>
        /// Obtains the data of the authenticated employee, if applicable
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-authenticated-employee-data")]
        [ProducesResponseType(typeof(EmployeeClaimsViewModel), 200)]
        [ProducesResponseType(typeof(void), 401)]
        public IActionResult GetAuthenticatedEmployeeData()
        {
            var employeeClaims = _authDomainService.ReadUserClaims(User.Claims.ToList());

            return PersonalizedResponse(Ok(employeeClaims));
        }
    }
}
