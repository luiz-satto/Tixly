using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tixly.Infrastructure.Models;

namespace Tixly.Infrastructure.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasColumnName(nameof(Event.Name))
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Venue)
                .HasColumnName(nameof(Event.Venue))
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Description)
                .HasColumnName(nameof(Event.Description))
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany<Ticket>()
                .WithOne(p => p.Event)
                .HasForeignKey(e => e.EventId)
                .IsRequired();

            builder.HasMany(e => e.PricingTiers)
                .WithOne()
                .HasForeignKey(e => e.EventId)
                .IsRequired();
        }
    }
}
