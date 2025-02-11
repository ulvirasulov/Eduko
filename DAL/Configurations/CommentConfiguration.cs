using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(c => c.Review)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(c => c.Rating)
            .IsRequired()
            .HasPrecision(3, 2);

        builder.Property(c => c.Date)
            .IsRequired();

        builder.HasOne(c => c.Course)
            .WithMany(c => c.Comments)
            .HasForeignKey(c => c.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Student)
            .WithMany(s => s.Comments)
            .HasForeignKey(c => c.StudentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(c => c.AppUser)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}