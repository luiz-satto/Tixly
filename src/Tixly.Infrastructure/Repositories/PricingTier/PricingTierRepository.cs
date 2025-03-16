using Microsoft.EntityFrameworkCore;
using Tixly.Infrastructure.Data;

namespace Tixly.Infrastructure.Repositories.PricingTier
{
    public sealed class PricingTierRepository(AppDbContext dbContext)
        : RepositoryBase<Models.PricingTier>(dbContext), IPricingTierRepository
    {
        private AppDbContext AppDbContext { get; init; } = dbContext;

        public async Task<IEnumerable<Models.PricingTier>> GetAsync(CancellationToken cancellationToken)
        {
            var result = await AppDbContext.PricingTiers
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return result ?? default!;
        }

        public async Task<Models.PricingTier> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await AppDbContext.PricingTiers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return result ?? default!;
        }
    }
}
