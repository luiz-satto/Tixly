using BuildingBlocks.Abstractions.Primitives;

namespace Tixly.Infrastructure.Models
{
    public class Ticket : Entity
    {
        public decimal Price { get; set; }
        public string Status { get; set; } = default!;

        public Guid EventId { get; set; }
        public Event? Event { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
