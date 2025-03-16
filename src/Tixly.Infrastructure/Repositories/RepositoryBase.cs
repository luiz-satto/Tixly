using BuildingBlocks.Abstractions;
using BuildingBlocks.Abstractions.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Tixly.Infrastructure.Data;

namespace Tixly.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity>(AppDbContext dbContext) : IEntityRepository<TEntity>
        where TEntity : Entity
    {
        private readonly AppDbContext _dbContext = dbContext;

        public virtual async Task<Guid> InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            EntityEntry<TEntity> result = _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result.Entity.Id;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            TEntity updatedEntity = _dbContext.Update(entity).Entity;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return updatedEntity;
        }

        public virtual async Task<bool> DeleteAsync(Guid entityId, CancellationToken cancellationToken)
        {
            TEntity? entity = _dbContext.Find<TEntity>(entityId);
            if (entity is null)
            {
                return false;
            }

            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
