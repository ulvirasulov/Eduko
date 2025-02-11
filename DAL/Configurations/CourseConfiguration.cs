using Core.Entities;
using Core.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(c => c.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(c => c.ImgUrl)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(c => c.Language)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.SkillLevel)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Status)
            .IsRequired()
            .HasDefaultValue(CourseStatus.Draft);

        builder.Property(c => c.AverageRating)
            .HasPrecision(3, 2)
            .HasDefaultValue(0);

        builder.Property(c => c.EnrolledStudentsCount)
            .HasDefaultValue(0);

        builder.HasOne(c => c.Category)
            .WithMany(cat => cat.Courses)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.EnrolledStudents)
            .WithMany(u => u.EnrolledCourses);
    }
}