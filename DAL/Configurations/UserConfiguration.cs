using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.ProfileImageUrl)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(u => u.Bio)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.HasMany(u => u.Comments)
            .WithOne(c => c.AppUser)
            .HasForeignKey(c => c.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}