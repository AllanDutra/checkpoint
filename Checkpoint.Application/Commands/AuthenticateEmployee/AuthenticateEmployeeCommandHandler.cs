using System.Net;
using Checkpoint.Core.DomainServices.Auth;
using Checkpoint.Core.DomainServices.Crypto;
using Checkpoint.Core.Interfaces.Notifications;
using Checkpoint.Core.Interfaces.Repositories;
using Checkpoint.Core.Models.ViewModels;
using MediatR;

namespace Checkpoint.Application.Commands.AuthenticateEmployee
{
    public class AuthenticateEmployeeCommandHandler
        : IRequestHandler<AuthenticateEmployeeCommand, EmployeeAuthenticationViewModel>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICryptoDomainService _cryptoDomainService;
        private readonly IAuthDomainService _authDomainService;
        private readonly INotifier _notifier;

        public AuthenticateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            ICryptoDomainService cryptoDomainService,
            IAuthDomainService authDomainService,
            INotifier notifier
        )
        {
            _employeeRepository = employeeRepository;
            _cryptoDomainService = cryptoDomainService;
            _authDomainService = authDomainService;
            _notifier = notifier;
        }

        public async Task<EmployeeAuthenticationViewModel> Handle(
            AuthenticateEmployeeCommand request,
            CancellationToken cancellationToken
        )
        {
            var encryptedPassword = _cryptoDomainService.EncryptToSha256(request.Password);

            var employeeClaims = await _employeeRepository.GetEmployeeClaimsByUsernameAndPassword(
                request.User,
                encryptedPassword
            );

            if (employeeClaims == null)
            {
                _notifier.Handle(
                    new NotificationModel("Usuário não encontrado", HttpStatusCode.NotFound)
                );

                return null;
            }

            var jwtToken = _authDomainService.GenerateJwtToken(employeeClaims);

            return new EmployeeAuthenticationViewModel(employeeClaims, jwtToken);
        }
    }
}
