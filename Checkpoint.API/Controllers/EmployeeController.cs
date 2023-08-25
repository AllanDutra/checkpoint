using Checkpoint.Application.Commands.GenerateEmailConfirmationCode;
using Checkpoint.Application.Queries.GetEmployeeInfo;
using Checkpoint.Application.Queries.GetInfoFromOtherEmployees;
using Checkpoint.Core.DomainServices.Auth;
using Checkpoint.Core.Interfaces.Notifications;
using Checkpoint.Core.Models.InputModels;
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

        /// <summary>
        /// Get id, name and status from the others employees
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<EmployeeInfoViewModel>), 200)]
        [ProducesResponseType(typeof(void), 401)]
        [HttpGet("get-info-from-other-employees")]
        public async Task<IActionResult> GetInfoFromOtherEmployeesAsync(
            [FromQuery] GetInfoFromOtherEmployeesInputModel inputModel
        )
        {
            var employeeClaims = _authDomainService.ReadUserClaims(User.Claims);

            var query = new GetInfoFromOtherEmployeesQuery(
                employeeClaims.Id,
                inputModel.Search,
                inputModel.Filter,
                inputModel.Ordination
            );

            var infoFromOtherEmployees = await _mediator.Send(query);

            return PersonalizedResponse(Ok(infoFromOtherEmployees));
        }

        /// <summary>
        /// Generates a new confirmation code and sends it to the authenticated employee's email
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(DefaultResponseViewModel), 200)]
        [ProducesResponseType(typeof(void), 401)]
        [ProducesResponseType(typeof(DefaultResponseViewModel), 424)]
        [HttpPut("generate-email-confirmation-code")]
        public async Task<IActionResult> GenerateEmailConfirmationCodeAsync()
        {
            var employeeClaims = _authDomainService.ReadUserClaims(User.Claims);

            var command = new GenerateEmailConfirmationCodeCommand(employeeClaims.Email);

            await _mediator.Send(command);

            return PersonalizedResponse(Ok());
        }
    }
}
