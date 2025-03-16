using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tixly.Infrastructure.Models;

namespace Tixly.Infrastructure.Data.Configurations
{
    public class PricingTierConfiguration : IEntityTypeConfiguration<PricingTier>
    {
        public void Configure(EntityTypeBuilder<PricingTier> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasColumnName(nameof(PricingTier.Name))
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Benefits)
                .HasColumnName(nameof(PricingTier.Benefits))
                .IsRequired();

            builder.HasOne<Event>()
                .WithMany(e => e.PricingTiers)
                .HasForeignKey(p => p.EventId)
                .IsRequired();
        }
    }
}
