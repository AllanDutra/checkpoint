namespace Checkpoint.Core.Models.ViewModels
{
    public class EmployeeClaimsViewModel
    {
        public EmployeeClaimsViewModel(
            int id,
            string email,
            string name,
            string user,
            bool? verifiedEmail
        )
        {
            Id = id;
            Email = email;
            Name = name;
            User = user;
            VerifiedEmail = verifiedEmail;
        }

        public int Id { get; }

        public string Email { get; } = null!;

        public string Name { get; } = null!;

        public string User { get; } = null!;

        public bool? VerifiedEmail { get; }
    }
}
