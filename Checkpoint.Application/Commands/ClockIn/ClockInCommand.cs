using MediatR;

namespace Checkpoint.Application.Commands.ClockIn
{
    public class ClockInCommand : IRequest<Unit>
    {
        public ClockInCommand(int employeeId, char type)
        {
            EmployeeId = employeeId;
            Type = type;
        }

        public int EmployeeId { get; }
        public char Type { get; } // ? PointLogTypeEnum
    }
}
