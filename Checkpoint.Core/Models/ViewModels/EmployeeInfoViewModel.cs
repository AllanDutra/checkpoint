namespace Checkpoint.Core.Models.ViewModels
{
    public class EmployeeInfoViewModel
    {
        public EmployeeInfoViewModel(int id, string name, string status)
        {
            Id = id;
            Name = name;
            Status = status;
        }

        public int Id { get; }
        public string Name { get; }
        public string Status { get; } // ? Available or Last Seen on...
    }
}
