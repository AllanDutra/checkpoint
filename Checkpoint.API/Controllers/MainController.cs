using Checkpoint.Core.DomainServices.Auth;
using Checkpoint.Core.Interfaces.Notifications;
using Checkpoint.Core.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Checkpoint.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly INotifier _notifier;
        protected readonly IAuthDomainService _authDomainService;

        public MainController(
            IMediator mediator,
            INotifier notifier,
            IAuthDomainService authDomainService
        )
        {
            _mediator = mediator;
            _notifier = notifier;
            _authDomainService = authDomainService;
        }

        protected ActionResult PersonalizedResponse(ActionResult actionResult)
        {
            if (ValidOperation())
                return actionResult;

            var notification = _notifier.GetNotifications().First();

            Response.StatusCode = (int)notification.StatusCode;

            return new JsonResult(new DefaultResponseViewModel(notification.Message));
        }

        private bool ValidOperation() => !_notifier.HasNotification();
    }
}
