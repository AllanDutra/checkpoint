using MediatR;

namespace Checkpoint.Application.Queries.GetEmployeeEmailAlreadyVerified
{
    public class GetEmployeeEmailAlreadyVerifiedQuery : IRequest<bool>
    {
        public GetEmployeeEmailAlreadyVerifiedQuery(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
