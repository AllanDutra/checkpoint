using Checkpoint.API.Attributes;
using Checkpoint.Application.Commands.ConfirmEmail;
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
        [ProducesResponseType(typeof(DefaultResponseViewModel), 403)]
        [RequiresConfirmedEmail]
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
        [ProducesResponseType(typeof(DefaultResponseViewModel), 400)]
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

        /// <summary>
        /// Confirms the email using the confirmation code sent to the employee's email and returns the new jwt with verified email field as true
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(DefaultResponseViewModel), 400)]
        [ProducesResponseType(typeof(void), 401)]
        [ProducesResponseType(typeof(DefaultResponseViewModel), 404)]
        [HttpPut("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync(
            [FromQuery] ConfirmEmailInputModel inputModel
        )
        {
            var employeeClaims = _authDomainService.ReadUserClaims(User.Claims);

            var command = new ConfirmEmailCommand(
                employeeClaims.Email,
                inputModel.ConfirmationCode
            );

            var newJwtToken = await _mediator.Send(command);

            return PersonalizedResponse(Ok(newJwtToken));
        }
    }
}
