using System.Net;
using Checkpoint.Core.DomainServices.Auth;
using Checkpoint.Core.Interfaces.Notifications;
using Checkpoint.Core.Interfaces.Repositories;
using Checkpoint.Core.Models.ViewModels;
using MediatR;

namespace Checkpoint.Application.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, string>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmailVerificationRepository _emailVerificationRepository;
        private readonly INotifier _notifier;
        private readonly IAuthDomainService _authDomainService;

        public ConfirmEmailCommandHandler(
            IEmployeeRepository employeeRepository,
            IEmailVerificationRepository emailVerificationRepository,
            INotifier notifier,
            IAuthDomainService authDomainService
        )
        {
            _employeeRepository = employeeRepository;
            _emailVerificationRepository = emailVerificationRepository;
            _notifier = notifier;
            _authDomainService = authDomainService;
        }

        public async Task<string> Handle(
            ConfirmEmailCommand request,
            CancellationToken cancellationToken
        )
        {
            var employee = await _employeeRepository.GetByEmailAsync(request.EmployeeEmail);

            bool verifiedEmail = employee.VerifiedEmail ?? false;

            if (verifiedEmail)
            {
                _notifier.Handle(
                    new NotificationModel(
                        "Your email has already been verified!",
                        HttpStatusCode.BadRequest
                    )
                );

                return null;
            }

            var registeredEmailVerification =
                await _emailVerificationRepository.GetByEmployeeEmailAsync(request.EmployeeEmail);

            if (registeredEmailVerification == null)
            {
                _notifier.Handle(
                    new NotificationModel(
                        "No verification code was found for your email, please generate a new code and try again",
                        HttpStatusCode.NotFound
                    )
                );

                return null;
            }

            if (request.ConfirmationCode != registeredEmailVerification.VerificationCode)
            {
                _notifier.Handle(
                    new NotificationModel(
                        "Confirmation code do not match.",
                        HttpStatusCode.BadRequest
                    )
                );

                return null;
            }

            employee.VerifiedEmail = true;

            await _employeeRepository.SaveChangesAsync();

            await _emailVerificationRepository.DeleteByEmployeeEmailAsync(request.EmployeeEmail);

            var newJwtToken = _authDomainService.GenerateJwtToken(
                new EmployeeClaimsViewModel(
                    employee.Id,
                    employee.Email,
                    employee.Name,
                    employee.User,
                    true
                )
            );

            return newJwtToken;
        }
    }
}
