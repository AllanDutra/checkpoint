namespace Checkpoint.Core.DomainServices.ExpiredEmailConfirmationChecker
{
    public interface IExpiredEmailConfirmationCheckerDomainService
    {
        public Task DoWork(CancellationToken stoppingToken);
    }
}
