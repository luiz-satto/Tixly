using Microsoft.EntityFrameworkCore;
using Tixly.Infrastructure.Models;

namespace Tixly.Infrastructure.Data
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Event> Events { get; }
        DbSet<Ticket> Tickets { get; }
        DbSet<PricingTier> PricingTiers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
