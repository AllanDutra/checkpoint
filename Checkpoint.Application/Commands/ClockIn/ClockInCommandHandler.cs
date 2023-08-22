using System.Net;
using Checkpoint.Core.Entities;
using Checkpoint.Core.Interfaces.Notifications;
using Checkpoint.Core.Interfaces.Repositories;
using Checkpoint.Core.Models.ViewModels;
using MediatR;

namespace Checkpoint.Application.Commands.ClockIn
{
    public class ClockInCommandHandler : IRequestHandler<ClockInCommand, Unit>
    {
        private readonly IPointLogRepository _pointLogRepository;
        private readonly INotifier _notifier;

        public ClockInCommandHandler(IPointLogRepository pointLogRepository, INotifier notifier)
        {
            _pointLogRepository = pointLogRepository;
            _notifier = notifier;
        }

        public async Task<Unit> Handle(ClockInCommand request, CancellationToken cancellationToken)
        {
            var pointLog = new PointLog(request.EmployeeId, DateTime.Now, request.Type);

            var lastCheckpoint = await _pointLogRepository.GetLastCheckpoint(request.EmployeeId);

            if (!pointLog.NewCheckpointTypeIsValid(lastCheckpoint?.Type))
            {
                _notifier.Handle(
                    new NotificationModel(
                        "The new type has to be different from the previous one as it is not possible to mark two consecutive arrival or exit checkpoints",
                        HttpStatusCode.BadRequest
                    )
                );

                return Unit.Value;
            }

            await _pointLogRepository.RegisterAsync(pointLog);

            return Unit.Value;
        }
    }
}
