using Checkpoint.Application.Queries;
using Checkpoint.Core.DomainServices.Auth;
using Checkpoint.Core.Interfaces.Notifications;
using Checkpoint.Core.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkpoint.API.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : MainController
    {
        public EmployeeController(
            IMediator mediator,
            INotifier notifier,
            IAuthDomainService authDomainService
        )
            : base(mediator, notifier, authDomainService) { }

        /// <summary>
        /// Get employee profile info
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(EmployeeInfoViewModel), 200)]
        [ProducesResponseType(typeof(void), 401)]
        [HttpGet("get-info")]
        public async Task<IActionResult> GetInfoAsync()
        {
            var employeeClaims = _authDomainService.ReadUserClaims(User.Claims);

            var query = new GetEmployeeInfoQuery(employeeClaims.Id, employeeClaims.Name);

            var employeeInfo = await _mediator.Send(query);

            return PersonalizedResponse(Ok(employeeInfo));
        }
    }
}
