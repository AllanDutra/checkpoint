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
    }
}
