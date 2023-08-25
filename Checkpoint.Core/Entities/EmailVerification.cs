namespace Checkpoint.Core.Entities;

public partial class EmailVerification
{
    public EmailVerification(string employeeEmail, string verificationCode)
    {
        EmployeeEmail = employeeEmail;
        VerificationCode = verificationCode;
        GenerationDate = DateTime.Now;
    }

    public EmailVerification(
        int id,
        string employeeEmail,
        string verificationCode,
        DateTime generationDate
    )
    {
        Id = id;
        EmployeeEmail = employeeEmail;
        VerificationCode = verificationCode;
        GenerationDate = generationDate;
    }

    public int Id { get; set; }

    public string EmployeeEmail { get; set; } = null!;

    public string VerificationCode { get; set; } = null!;

    public DateTime GenerationDate { get; set; }

    public virtual Employee EmployeeEmailNavigation { get; set; } = null!;
}
