namespace Tixly.Infrastructure.Repositories.User
{
    public interface IUserRepository
    {
        Task<IEnumerable<Models.User>> GetAsync(CancellationToken cancellationToken);
        Task<Models.User> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> InsertAsync(Models.User user, CancellationToken cancellationToken);
        Task<Models.User> UpdateAsync(Models.User user, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
