namespace Tixly.Infrastructure.Repositories.Event
{
    public interface IEventRepository
    {
        Task<IEnumerable<Models.Event>> GetAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<Models.Event> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> InsertAsync(Models.Event @event, CancellationToken cancellationToken);
        Task<Models.Event> UpdateAsync(Models.Event @event, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
