using Checkpoint.Application.Commands.ClockIn;
using Checkpoint.Core.DomainServices.Auth;
using Checkpoint.Core.Interfaces.Notifications;
using Checkpoint.Core.Models.InputModels;
using Checkpoint.Core.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkpoint.API.Controllers
{
    [ApiController]
    [Route("api/checkpoint")]
    public class CheckpointController : MainController
    {
        public CheckpointController(
            IMediator mediator,
            INotifier notifier,
            IAuthDomainService authDomainService
        )
            : base(mediator, notifier, authDomainService) { }

        /// <summary>
        /// Register a new point log for the authenticated employee
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(DefaultResponseViewModel), 400)]
        [ProducesResponseType(typeof(void), 401)]
        [HttpPost("clock-in")]
        public async Task<IActionResult> ClockInAsync([FromQuery] ClockInInputModel inputModel)
        {
            var employeeClaims = _authDomainService.ReadUserClaims(User.Claims);

            var command = new ClockInCommand(employeeClaims.Id, inputModel.CheckpointType);

            await _mediator.Send(command);

            return PersonalizedResponse(NoContent());
        }
    }
}
