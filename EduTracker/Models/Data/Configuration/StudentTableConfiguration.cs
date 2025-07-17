using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduTracker.Models.Data.Configuration;

public class StudentTableConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id).ValueGeneratedOnAdd();

        builder.Property(s => s.StudentId).HasMaxLength(20).IsRequired();

        builder.Property(s => s.StudentName).HasMaxLength(255).IsRequired();
        
        builder.Property(s => s.StudentState).HasMaxLength(200).IsRequired();

        builder.Property(s => s.TotalGrade).HasPrecision(15, 2).IsRequired();

        builder.HasIndex(s => s.StudentId);
        builder.HasIndex(s => s.StudentName);
        
        builder.ToTable("Students");
    }
}