using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

public class WorkHourConfiguration : IEntityTypeConfiguration<WorkHour>
{
    public void Configure(EntityTypeBuilder<WorkHour> builder)
    {
        builder.ToTable("WorkHours").HasKey(w => w.Id);

        builder.Property(w => w.Id).HasColumnName("Id").IsRequired();
        builder.Property(w => w.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(w => w.StartHour).HasColumnName("StartHour").IsRequired();
        builder.Property(w => w.EndHour).HasColumnName("EndHour").IsRequired();
        builder.Property(w => w.StudyDate).HasColumnName("StudyDate").IsRequired();

        builder.HasIndex(indexExpression: w => w.Id, name: "UK_Id").IsUnique();
        builder.HasIndex(indexExpression: u => u.UserId, name: "UK_UserId").IsUnique();


        builder.HasQueryFilter(w => !w.DeletedDate.HasValue);

    }
}