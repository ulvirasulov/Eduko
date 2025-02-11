using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.Description)
            .IsRequired();

        builder.Property(b => b.ImgUrl)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(b => b.Date)
            .IsRequired();

        builder.Property(b => b.TeacherOpinion)
            .IsRequired(false)
            .HasMaxLength(2000);

        builder.HasMany(b => b.Tags)
            .WithMany(t => t.Blogs);
    }
}