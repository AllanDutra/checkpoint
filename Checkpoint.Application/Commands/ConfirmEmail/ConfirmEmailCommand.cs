using MediatR;

namespace Checkpoint.Application.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<string>
    {
        public ConfirmEmailCommand(string employeeEmail, string confirmationCode)
        {
            EmployeeEmail = employeeEmail;
            ConfirmationCode = confirmationCode;
        }

        public string EmployeeEmail { get; }
        public string ConfirmationCode { get; }
    }
}
