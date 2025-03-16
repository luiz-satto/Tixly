using BuildingBlocks.Enums;
using Tixly.Infrastructure.Models;

namespace Tixly.Infrastructure.Data.Extensions
{
    internal static class InitialData
    {
        public static IEnumerable<User> Users =>
        [
            new User()
            {
                Id = new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@tixly.com",
                Password = "123456@",
                Role = (int)UserRole.ADMIN,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new User()
            {
                Id = new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
                FirstName = "Juca",
                LastName = "Jarbas",
                Email = "customer1@email.com",
                Password = "654123@",
                Role = (int)UserRole.CUSTOMER,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new User()
            {
                Id = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                FirstName = "Joselito",
                LastName = "Silva",
                Email = "customer2@email.com",
                Password = "654123@",
                Role = (int)UserRole.CUSTOMER,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        ];

        public static IEnumerable<Event> Events =>
        [
            new Event()
            {
                Id = new Guid("550e8400-e29b-41d4-a716-446655440000"),
                Name = "Music Festival",
                Venue = "Central Park",
                Description = "A grand music festival!",
                AvailableTickets = 198,
                TotalCapacity = 200,
                Date = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Event()
            {
                Id = new Guid("b87df7be-91e9-48e6-8df2-c6bea744e0b4"),
                Name = "Tech Summit",
                Venue = "Tech Arena",
                Description = "The greatest techno event!",
                AvailableTickets = 397,
                TotalCapacity = 400,
                Date = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        ];

        public static IEnumerable<PricingTier> PricingTiers =>
        [
            new PricingTier()
            {
                Id = new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
                EventId = new Guid("550e8400-e29b-41d4-a716-446655440000"),
                Name = "General Admission",
                Price = 50.00M,
                Capacity = 100,
                Benefits = ["Event Entry"],
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new PricingTier()
            {
                Id = new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8"),
                EventId = new Guid("550e8400-e29b-41d4-a716-446655440000"),
                Name = "VIP",
                Price = 120.00M,
                Capacity = 35,
                Benefits = ["Event Entry", "Complimentary Drink", "Priority Seating"],
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new PricingTier()
            {
                Id = new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27"),
                EventId = new Guid("550e8400-e29b-41d4-a716-446655440000"),
                Name = "VVIP",
                Price = 200.00M,
                Capacity = 15,
                Benefits =  ["Event Entry", "Complimentary Drink", "Priority Seating", "Meet & Greet with Speakers"],
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new PricingTier()
            {
                Id = Guid.NewGuid(),
                EventId = new Guid("b87df7be-91e9-48e6-8df2-c6bea744e0b4"),
                Name = "General Admission",
                Price = 50.00M,
                Capacity = 100,
                Benefits = ["Event Entry"],
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new PricingTier()
            {
                Id = Guid.NewGuid(),
                EventId = new Guid("b87df7be-91e9-48e6-8df2-c6bea744e0b4"),
                Name = "VIP",
                Price = 120.00M,
                Capacity = 35,
                Benefits = ["Event Entry", "Complimentary Drink", "Priority Seating"],
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new PricingTier()
            {
                Id = Guid.NewGuid(),
                EventId = new Guid("b87df7be-91e9-48e6-8df2-c6bea744e0b4"),
                Name = "VVIP",
                Price = 200.00M,
                Capacity = 15,
                Benefits =  ["Event Entry", "Complimentary Drink", "Priority Seating", "Meet & Greet with Speakers"],
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        ];

        public static IEnumerable<Ticket> Tickets => 
        [
            new Ticket() 
            {
                Id = Guid.NewGuid(),
                EventId = new Guid("550e8400-e29b-41d4-a716-446655440000"),
                UserId = new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
                Price = 50,
                Status = TicketStatus.Booked,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Ticket()
            {
                Id = Guid.NewGuid(),
                EventId = new Guid("550e8400-e29b-41d4-a716-446655440000"),
                UserId = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                Price = 200,
                Status = TicketStatus.Canceled,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Ticket()
            {
                Id = Guid.NewGuid(),
                EventId = new Guid("550e8400-e29b-41d4-a716-446655440000"),
                UserId = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                Price = 120,
                Status = TicketStatus.Booked,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Ticket()
            {
                Id = Guid.NewGuid(),
                EventId = new Guid("b87df7be-91e9-48e6-8df2-c6bea744e0b4"),
                UserId = new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
                Price = 120,
                Status = TicketStatus.Booked,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Ticket()
            {
                Id = Guid.NewGuid(),
                EventId = new Guid("b87df7be-91e9-48e6-8df2-c6bea744e0b4"),
                UserId = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                Price = 200,
                Status = TicketStatus.Booked,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Ticket()
            {
                Id = Guid.NewGuid(),
                EventId = new Guid("b87df7be-91e9-48e6-8df2-c6bea744e0b4"),
                UserId = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                Price = 200,
                Status = TicketStatus.Booked,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
                        new Ticket()
            {
                Id = Guid.NewGuid(),
                EventId = new Guid("b87df7be-91e9-48e6-8df2-c6bea744e0b4"),
                UserId = new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
                Price = 50,
                Status = TicketStatus.Canceled,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
        ];
    }
}
