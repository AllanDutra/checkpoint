using Checkpoint.Core.Interfaces.Repositories;
using Checkpoint.Core.Models.ViewModels;
using Checkpoint.Shared.Utils;
using MediatR;

namespace Checkpoint.Application.Queries.GetEmployeeInfo
{
    public class GetEmployeeInfoQueryHandler
        : IRequestHandler<GetEmployeeInfoQuery, EmployeeInfoViewModel>
    {
        private readonly IPointLogRepository _pointLogRepository;

        public GetEmployeeInfoQueryHandler(IPointLogRepository pointLogRepository)
        {
            _pointLogRepository = pointLogRepository;
        }

        public async Task<EmployeeInfoViewModel> Handle(
            GetEmployeeInfoQuery request,
            CancellationToken cancellationToken
        )
        {
            var lastCheckpoint = await _pointLogRepository.GetLastCheckpoint(request.Id);

            var status = Formatting.GetEmployeeStatus((lastCheckpoint?.Type, lastCheckpoint?.Date));

            return new EmployeeInfoViewModel(request.Id, request.Name, status);
        }
    }
}
