using Checkpoint.Core.Models.ViewModels;
using MediatR;

namespace Checkpoint.Application.Queries.GetEmployeeInfo
{
    public class GetEmployeeInfoQuery : IRequest<EmployeeInfoViewModel>
    {
        public GetEmployeeInfoQuery(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}
