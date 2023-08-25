using Checkpoint.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checkpoint.Infrastructure.Persistence.Configurations
{
    public class EmailVerificationConfigurations : IEntityTypeConfiguration<EmailVerification>
    {
        public void Configure(EntityTypeBuilder<EmailVerification> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailVer__3213E83F203BABE2");

            entity.HasIndex(e => e.EmployeeEmail, "UQ__EmailVer__0A874BCFBC84712E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmployeeEmail).HasMaxLength(255).HasColumnName("employee_email");
            entity
                .Property(e => e.GenerationDate)
                .HasColumnType("datetime")
                .HasColumnName("generationDate");
            entity
                .Property(e => e.VerificationCode)
                .HasMaxLength(255)
                .HasColumnName("verificationCode");

            entity
                .HasOne(d => d.EmployeeEmailNavigation)
                .WithOne(p => p.EmailVerification)
                .HasPrincipalKey<Employee>(p => p.Email)
                .HasForeignKey<EmailVerification>(d => d.EmployeeEmail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmailVeri__emplo__52593CB8");
        }
    }
}
