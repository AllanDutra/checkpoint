using MediatR;

namespace Checkpoint.Application.Commands.RegisterEmployee
{
    public class RegisterEmployeeCommand : IRequest<Unit>
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }
}
