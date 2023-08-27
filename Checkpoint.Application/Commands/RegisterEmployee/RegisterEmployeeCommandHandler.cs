using System.Net;
using Checkpoint.Core.DomainServices.Crypto;
using Checkpoint.Core.Entities;
using Checkpoint.Core.Interfaces.Notifications;
using Checkpoint.Core.Interfaces.Repositories;
using Checkpoint.Core.Models.ViewModels;
using MediatR;

namespace Checkpoint.Application.Commands.RegisterEmployee
{
    public class RegisterEmployeeCommandHandler : IRequestHandler<RegisterEmployeeCommand, Unit>
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly ICryptoDomainService _cryptoDomainService;

        private readonly INotifier _notifier;

        public RegisterEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            ICryptoDomainService cryptoDomainService,
            INotifier notifier
        )
        {
            _employeeRepository = employeeRepository;
            _cryptoDomainService = cryptoDomainService;
            _notifier = notifier;
        }

        public async Task<Unit> Handle(
            RegisterEmployeeCommand request,
            CancellationToken cancellationToken
        )
        {
            var employee = new Employee(
                request.Email,
                request.Name,
                request.User,
                _cryptoDomainService.EncryptToSha256(request.Password),
                DateTime.Now
            );

            if (await _employeeRepository.AlreadyAnEmployeeWithTheSameEmailAsync(request.Email))
            {
                _notifier.Handle(
                    new NotificationModel(
                        "There is already an employee with the entered email.",
                        HttpStatusCode.BadRequest
                    )
                );

                return Unit.Value;
            }

            if (await _employeeRepository.AlreadyAnEmployeeWithTheSameUsernameAsync(request.User))
            {
                _notifier.Handle(
                    new NotificationModel(
                        "There is already an employee with the entered username.",
                        HttpStatusCode.BadRequest
                    )
                );

                return Unit.Value;
            }

            await _employeeRepository.RegisterAsync(employee);

            return Unit.Value;
        }
    }
}
