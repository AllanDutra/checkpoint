using Checkpoint.Core.Enums;

namespace Checkpoint.Shared.Utils
{
    public static class Formatting
    {
        public static string GetDateInFull(DateTime date)
        {
            if (date == DateTime.Today)
                return "Today";

            if (date == DateTime.Today.AddDays(-1))
                return "Yesterday";

            return $"on {date:MM/dd/yyyy}";
        }

        public static string GetEmployeeStatus((char? Type, DateTime? Date) lastPointLog)
        {
            if (lastPointLog.Type == null || lastPointLog.Date == null)
                return "Unavailable";

            if ((PointLogTypeEnum)lastPointLog.Type == PointLogTypeEnum.Arrival)
                return "Available";

            var date = (DateTime)lastPointLog.Date;

            return $"Last seen {GetDateInFull(date.Date)} at {date.Hour}:{date.Minute} {date:tt}".TrimEnd();
        }
    }
}
