namespace Checkpoint.Core.Entities;

public class Employee
{
    public Employee(string email, string name, string user, string password)
    {
        Email = email;
        Name = name;
        User = user;
        Password = password;
    }

    public Employee(int id, string email, string name, string user, string password)
    {
        Id = id;
        Email = email;
        Name = name;
        User = user;
        Password = password;
    }

    public int Id { get; }

    public string Email { get; } = null!;

    public string Name { get; } = null!;

    public string User { get; } = null!;

    public string Password { get; } = null!;

    public bool? VerifiedEmail { get; set; }

    public virtual EmailVerification? EmailVerification { get; }

    public virtual ICollection<PointLog> PointLogs { get; } = new List<PointLog>();
}
