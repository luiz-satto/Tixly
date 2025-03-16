using BuildingBlocks.Abstractions.Primitives;

namespace BuildingBlocks.Abstractions
{
    public interface IEntityRepository<TEntity>
        where TEntity : Entity
    {
        Task<Guid> InsertAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid entityId, CancellationToken cancellationToken);
    }
}
