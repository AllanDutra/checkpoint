using MediatR;

namespace Checkpoint.Application.Commands.GenerateEmailConfirmationCode
{
    public class GenerateEmailConfirmationCodeCommand : IRequest<Unit>
    {
        public GenerateEmailConfirmationCodeCommand(string employeeEmail)
        {
            EmployeeEmail = employeeEmail;
        }

        public string EmployeeEmail { get; }
    }
}
