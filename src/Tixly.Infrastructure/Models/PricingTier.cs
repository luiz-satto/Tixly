using BuildingBlocks.Abstractions.Primitives;

namespace Tixly.Infrastructure.Models
{
    public class PricingTier : Entity
    {
        public string Name { get; set; } = default!;
        public IEnumerable<string> Benefits { get; set; } = default!;
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public Guid EventId { get; set; }
    }
}
