using Checkpoint.Application.Commands.RegisterEmployee;
using Checkpoint.Core.Interfaces.Notifications;
using Checkpoint.Core.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkpoint.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : MainController
    {
        public AuthenticationController(IMediator mediator, INotifier notifier)
            : base(mediator, notifier) { }

        /// <summary>
        /// Register a new employee in database
        /// </summary>
        /// <param name="command"></param>
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
    }
}
