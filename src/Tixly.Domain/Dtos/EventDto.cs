using Tixly.Infrastructure.Models;

namespace Tixly.Domain.Dtos
{
    public record EventDto(
        Guid Id,
        string Name,
        string Venue,
        string Description,
        DateTime Date,
        int TotalCapacity,
        int AvailableTickets,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        ICollection<PricingTier> PricingTiers = default!
    );
}
