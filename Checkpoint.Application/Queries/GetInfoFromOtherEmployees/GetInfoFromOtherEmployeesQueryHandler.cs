using Checkpoint.Core.Interfaces.Repositories;
using Checkpoint.Core.Models.ViewModels;
using Checkpoint.Shared.Utils;
using MediatR;

namespace Checkpoint.Application.Queries.GetInfoFromOtherEmployees
{
    public class GetInfoFromOtherEmployeesQueryHandler
        : IRequestHandler<GetInfoFromOtherEmployeesQuery, List<EmployeeInfoViewModel>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetInfoFromOtherEmployeesQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeInfoViewModel>> Handle(
            GetInfoFromOtherEmployeesQuery request,
            CancellationToken cancellationToken
        )
        {
            var otherEmployeesInfo = await _employeeRepository.GetInfoFromOtherEmployeesAsync(
                request.IdEmployeeWhoIsQuerying,
                request.Search,
                request.Filter,
                request.Ordination
            );

            return otherEmployeesInfo
                .Select(
                    e =>
                        new EmployeeInfoViewModel(
                            e.Id,
                            e.Name,
                            Formatting.GetEmployeeStatus((e.Type, e.LastPointLogDate))
                        )
                )
                .ToList();
        }
    }
}
