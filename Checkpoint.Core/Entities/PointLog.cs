using Checkpoint.Core.Enums;

namespace Checkpoint.Core.Entities;

public class PointLog
{
    public PointLog(int id, int? empolyeeId, DateTime date, PointLogTypeEnum? type)
    {
        Id = id;
        EmpolyeeId = empolyeeId;
        Date = date;
        Type = type;
    }

    public int Id { get; }

    public int? EmpolyeeId { get; }

    public DateTime Date { get; }

    public PointLogTypeEnum? Type { get; }

    public virtual Employee? Empolyee { get; }
}
