namespace Checkpoint.Core.Entities;

public class Employee
{
    public Employee(
        string email,
        string name,
        string user,
        string password,
        DateTime? registrationDate
    )
    {
        Email = email;
        Name = name;
        User = user;
        Password = password;
        RegistrationDate = registrationDate;
    }

    public Employee(
        int id,
        string email,
        string name,
        string user,
        string password,
        DateTime? registrationDate
    )
    {
        Id = id;
        Email = email;
        Name = name;
        User = user;
        Password = password;
        RegistrationDate = registrationDate;
    }

    public int Id { get; }

    public string Email { get; } = null!;

    public string Name { get; } = null!;

    public string User { get; } = null!;

    public string Password { get; } = null!;

    public bool? VerifiedEmail { get; set; }

    public DateTime? RegistrationDate { get; }

    public virtual EmailVerification? EmailVerification { get; }

    public virtual ICollection<PointLog> PointLogs { get; } = new List<PointLog>();
}
