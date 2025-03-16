using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tixly.Infrastructure.Models;

namespace Tixly.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .HasColumnName(nameof(User.FirstName))
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .HasColumnName(nameof(User.LastName))
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Password)
                .HasColumnName(nameof(User.Password))
                .IsRequired()
                .HasMaxLength(12);

            builder.Property(u => u.Email)
                .HasColumnName(nameof(User.Email))
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasMany<Ticket>()
                .WithOne(p => p.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        }
    }
}
