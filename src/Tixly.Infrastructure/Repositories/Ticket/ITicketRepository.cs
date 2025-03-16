namespace Tixly.Infrastructure.Repositories.Ticket
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Models.Ticket>> GetAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<Models.Ticket> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> InsertAsync(Models.Ticket ticket, CancellationToken cancellationToken);
        Task<Models.Ticket> UpdateAsync(Models.Ticket ticket, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
