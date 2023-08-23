using Checkpoint.Core.Models.ViewModels;
using MediatR;

namespace Checkpoint.Application.Queries.GetInfoFromOtherEmployees
{
#nullable enable
    public class GetInfoFromOtherEmployeesQuery : IRequest<List<EmployeeInfoViewModel>>
    {
        public GetInfoFromOtherEmployeesQuery(
            int idEmployeeWhoIsQuerying,
            string? search,
            string? filter,
            string? ordination
        )
        {
            IdEmployeeWhoIsQuerying = idEmployeeWhoIsQuerying;
            Search = search;
            Filter = filter;
            Ordination = ordination;
        }

        public int IdEmployeeWhoIsQuerying { get; }
        public string? Search { get; }
        public string? Filter { get; }
        public string? Ordination { get; }
    }
}
