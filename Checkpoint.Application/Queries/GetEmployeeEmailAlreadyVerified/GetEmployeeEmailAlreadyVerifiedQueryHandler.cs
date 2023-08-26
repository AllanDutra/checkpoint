using Checkpoint.Core.Interfaces.Repositories;
using MediatR;

namespace Checkpoint.Application.Queries.GetEmployeeEmailAlreadyVerified
{
    public class GetEmployeeEmailAlreadyVerifiedQueryHandler
        : IRequestHandler<GetEmployeeEmailAlreadyVerifiedQuery, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeEmailAlreadyVerifiedQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(
            GetEmployeeEmailAlreadyVerifiedQuery request,
            CancellationToken cancellationToken
        ) => await _employeeRepository.EmployeeEmailAlreadyVerifiedAsync(request.Email);
    }
}
