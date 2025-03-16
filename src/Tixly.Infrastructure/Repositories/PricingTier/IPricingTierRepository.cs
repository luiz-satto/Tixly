namespace Tixly.Infrastructure.Repositories.PricingTier
{
    public interface IPricingTierRepository
    {
        Task<IEnumerable<Models.PricingTier>> GetAsync(CancellationToken cancellationToken);
        Task<Models.PricingTier> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> InsertAsync(Models.PricingTier pricingTier, CancellationToken cancellationToken);
        Task<Models.PricingTier> UpdateAsync(Models.PricingTier pricingTier, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
