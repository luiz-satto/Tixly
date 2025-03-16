using BuildingBlocks.Abstractions.Primitives;

namespace Tixly.Infrastructure.Models
{
    public class Event : Entity
    {
        public string Name { get; set; } = default!;
        public string Venue { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime Date { get; set; }
        public int TotalCapacity { get; set; }
        public int AvailableTickets { get; set; }
        public ICollection<PricingTier> PricingTiers { get; set; } = [];
    }
}
