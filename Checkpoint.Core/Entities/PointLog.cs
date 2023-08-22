using Checkpoint.Core.Enums;
using Checkpoint.Shared.Utils;

namespace Checkpoint.Core.Entities;

public class PointLog
{
    public PointLog(int? empolyeeId, DateTime date, char? type)
    {
        EmpolyeeId = empolyeeId;
        Date = date;
        Type = type;
    }

    public PointLog(int id, int? empolyeeId, DateTime date, char? type)
    {
        Id = id;
        EmpolyeeId = empolyeeId;
        Date = date;
        Type = type;
    }

    public bool NewCheckpointTypeIsValid(char? lastCheckpointType) => Type != lastCheckpointType;

    public string GetEmployeeStatus()
    {
        if (Type == (char)PointLogTypeEnum.Arrival)
            return "Available";

        return $"Last seen {Formatting.GetDateInFull(Date.Date)} at {Date.Hour}:{Date.Minute} {Date:tt}";
    }

    public int Id { get; }

    public int? EmpolyeeId { get; }

    public DateTime Date { get; }

    public char? Type { get; }

    public virtual Employee Empolyee { get; }
}
