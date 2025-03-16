using Microsoft.EntityFrameworkCore;
using Tixly.Infrastructure.Data;

namespace Tixly.Infrastructure.Repositories.Event
{
    public class EventRepository(AppDbContext dbContext)
        : RepositoryBase<Models.Event>(dbContext), IEventRepository
    {
        private AppDbContext AppDbContext { get; init; } = dbContext;

        public async Task<IEnumerable<Models.Event>> GetAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var result = await AppDbContext.Events
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .Include(x => x.PricingTiers)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return result ?? default!;
        }

        public async Task<Models.Event> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await AppDbContext.Events
                .Include(x => x.PricingTiers)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return result ?? default!;
        }
    }
}
