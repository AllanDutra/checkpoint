using Checkpoint.Core.Models.ViewModels;
using MediatR;

namespace Checkpoint.Application.Commands.AuthenticateEmployee
{
    public class AuthenticateEmployeeCommand : IRequest<EmployeeAuthenticationViewModel>
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}
