using Checkpoint.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checkpoint.Infrastructure.Persistence.Configurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83FD343D5BE");

            entity.HasIndex(e => e.User, "UQ__Employee__7FC76D72F1CF4C79").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Employee__AB6E6164006FDEFA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasMaxLength(255).HasColumnName("email");
            entity.Property(e => e.Name).HasMaxLength(255).HasColumnName("name");
            entity.Property(e => e.Password).HasMaxLength(255).HasColumnName("password");
            entity.Property(e => e.User).HasMaxLength(255).HasColumnName("user");
            entity.Property(e => e.VerifiedEmail).HasColumnName("verified_email");
        }
    }
}
