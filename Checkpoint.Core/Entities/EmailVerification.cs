namespace Checkpoint.Core.Entities;

public class EmailVerification
{
    public EmailVerification(int id, string employeeEmail, string verificationCode)
    {
        Id = id;
        EmployeeEmail = employeeEmail;
        VerificationCode = verificationCode;
    }

    public int Id { get; }

    public string EmployeeEmail { get; } = null!;

    public string VerificationCode { get; } = null!;

    public virtual Employee EmployeeEmailNavigation { get; } = null!;
}
