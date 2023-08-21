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

    public int Id { get; }

    public int? EmpolyeeId { get; }

    public DateTime Date { get; }

    public char? Type { get; }

    public virtual Employee Empolyee { get; }
}
