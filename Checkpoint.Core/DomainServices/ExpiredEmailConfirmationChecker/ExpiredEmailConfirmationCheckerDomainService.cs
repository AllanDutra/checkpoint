using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Checkpoint.Core.Interfaces;

namespace Checkpoint.Core.DomainServices.ExpiredEmailConfirmationChecker
{
    public class ExpiredEmailConfirmationCheckerDomainService
        : IExpiredEmailConfirmationCheckerDomainService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpiredEmailConfirmationCheckerDomainService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var employeeEmailsToDelete =
                    await _unitOfWork.Employees.GetEmailsWithRegistrationDateOlderThan7DaysAndEmailIsNotVerifiedAsync();

                if (employeeEmailsToDelete.Any())
                {
                    await _unitOfWork.EmailVerifications.DeleteByEmployeeEmailsAsync(
                        employeeEmailsToDelete
                    );

                    await _unitOfWork.CompleteAsync();

                    await _unitOfWork.Employees.DeleteByEmailsAsync(employeeEmailsToDelete);

                    await _unitOfWork.CompleteAsync();
                }

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                // ? It would be interesting to implement a logging here in the future
            }
        }

        public class ConsumeScopedServiceHostedDomainService : BackgroundService
        {
            private const int MILLISECONDS_IN_24_HOURS = 24 * 60 * 60 * 1000; // ? 1 hour has 60 minutes, 1 minute has 60 seconds and 1 second has 1000 milliseconds
            private readonly IServiceProvider _services;

            public ConsumeScopedServiceHostedDomainService(IServiceProvider services)
            {
                _services = services;
            }

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(MILLISECONDS_IN_24_HOURS, stoppingToken);

                    using var scope = _services.CreateScope();

                    var scopedProcessingService =
                        scope.ServiceProvider.GetRequiredService<IExpiredEmailConfirmationCheckerDomainService>();

                    await scopedProcessingService.DoWork(stoppingToken);
                }
            }
        }
    }
}
