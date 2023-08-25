using System.Net;
using Checkpoint.Core.DomainServices.Auth;
using Checkpoint.Core.DomainServices.Mail;
using Checkpoint.Core.Entities;
using Checkpoint.Core.Interfaces.Notifications;
using Checkpoint.Core.Interfaces.Repositories;
using Checkpoint.Core.Models.ViewModels;
using MediatR;

namespace Checkpoint.Application.Commands.GenerateEmailConfirmationCode
{
    public class GenerateEmailConfirmationCodeCommandHandler
        : IRequestHandler<GenerateEmailConfirmationCodeCommand, Unit>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAuthDomainService _authDomainService;
        private readonly IEmailVerificationRepository _emailVerificationRepository;
        private readonly IMailDomainService _mailDomainService;
        private readonly INotifier _notifier;

        public GenerateEmailConfirmationCodeCommandHandler(
            IEmployeeRepository employeeRepository,
            IAuthDomainService authDomainService,
            IEmailVerificationRepository emailVerificationRepository,
            IMailDomainService mailDomainService,
            INotifier notifier
        )
        {
            _employeeRepository = employeeRepository;
            _authDomainService = authDomainService;
            _emailVerificationRepository = emailVerificationRepository;
            _mailDomainService = mailDomainService;
            _notifier = notifier;
        }

        public async Task<Unit> Handle(
            GenerateEmailConfirmationCodeCommand request,
            CancellationToken cancellationToken
        )
        {
            var employeeEmailAlreadyVerified =
                await _employeeRepository.EmployeeEmailAlreadyVerifiedAsync(request.EmployeeEmail);

            if (employeeEmailAlreadyVerified)
            {
                _notifier.Handle(
                    new NotificationModel(
                        "Your email has already been verified!",
                        HttpStatusCode.BadRequest
                    )
                );

                return Unit.Value;
            }

            var confirmationCode = _authDomainService.GenerateEmailConfirmationCode();

            var emailVerification = await _emailVerificationRepository.GetByEmployeeEmailAsync(
                request.EmployeeEmail
            );

            if (emailVerification == null)
            {
                var newEmailVerification = new EmailVerification(
                    request.EmployeeEmail,
                    confirmationCode
                );

                await _emailVerificationRepository.RegisterAsync(newEmailVerification);
            }
            else
            {
                emailVerification.VerificationCode = confirmationCode;

                await _emailVerificationRepository.SaveChangesAsync();
            }

            _mailDomainService.SendEmail(
                request.EmployeeEmail,
                $"Your confirmation code of Checkpoint platform: <br><strong>{confirmationCode}</strong>",
                "Checkpoint - Email Confirmation"
            );

            _notifier.Handle(
                new NotificationModel(
                    "A confirmation code has been generated and send to your email, check your email box to confirm your registration! You have up to 7 days to confirm, otherwise your registration will be deleted.",
                    HttpStatusCode.OK
                )
            );

            return Unit.Value;
        }
    }
}
