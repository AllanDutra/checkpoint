namespace Checkpoint.Core.Models.InputModels
{
#nullable enable
    public class GetInfoFromOtherEmployeesInputModel
    {
        /// <summary>
        /// Id or name of the employee
        /// </summary>
        public string? Search { get; set; }

        /// <summary>
        /// Filter by employee Status, "Available" or "Unavailable"
        /// </summary>
        public string? Filter { get; set; }

        /// <summary>
        /// Ordination of the list per employee name, being "ASC" for ascending or "DESC" for descending
        /// </summary>
        public string? Ordination { get; set; }
    }
}
