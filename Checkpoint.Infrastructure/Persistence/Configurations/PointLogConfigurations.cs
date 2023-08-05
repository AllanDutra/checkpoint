using Checkpoint.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checkpoint.Infrastructure.Persistence.Configurations
{
    public class PointLogConfigurations : IEntityTypeConfiguration<PointLog>
    {
        public void Configure(EntityTypeBuilder<PointLog> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__PointLog__3213E83F078068FE");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date).HasColumnType("datetime").HasColumnName("date");
            entity.Property(e => e.EmpolyeeId).HasColumnName("empolyee_id");
            entity
                .Property(e => e.Type)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("type");

            entity
                .HasOne(d => d.Empolyee)
                .WithMany(p => p.PointLogs)
                .HasForeignKey(d => d.EmpolyeeId)
                .HasConstraintName("FK__PointLogs__empol__4E88ABD4");
        }
    }
}
