using BuildingBlocks.Enums;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using Tixly.Infrastructure.Data;
using Tixly.Infrastructure.Models;

namespace Tixly.Infrastructure.Repositories.Ticket
{
    public class TicketRepository(AppDbContext dbContext)
        : RepositoryBase<Models.Ticket>(dbContext), ITicketRepository
    {
        private AppDbContext AppDbContext { get; init; } = dbContext;

        public async Task<IEnumerable<Models.Ticket>> GetAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var query = AppDbContext.Tickets
                .Skip(pageSize * pageIndex)
                .Take(pageSize);

            var result = await query
                .Include(x => x.Event)
                .Include(x => x.User)
                .AsNoTracking()
                .OrderBy(x => x.EventId)
                .ToListAsync(cancellationToken);

            return result ?? default!;
        }

        public async Task<Models.Ticket> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await AppDbContext.Tickets
                .Include(x => x.Event)
                .Include(x => x.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return result ?? default!;
        }

        public override async Task<Guid> InsertAsync(Models.Ticket ticket, CancellationToken cancellationToken)
        {
            var @event = await AppDbContext.Events.SingleAsync(x => x.Id == ticket.EventId, cancellationToken);
            @event.AvailableTickets--;
            return await base.InsertAsync(ticket, cancellationToken);
        }

        public override async Task<Models.Ticket> UpdateAsync(Models.Ticket ticket, CancellationToken cancellationToken)
        {
            if (await AppDbContext.Tickets.AnyAsync(x => x.Id == ticket.Id && x.Status == ticket.Status, cancellationToken))
            {
                var @event = await AppDbContext.Events.SingleAsync(x => x.Id == ticket.EventId, cancellationToken);
                if (ticket.Status == TicketStatus.Canceled)
                    @event.AvailableTickets++;
                else
                    @event.AvailableTickets--;
            }

            return await base.UpdateAsync(ticket, cancellationToken);
        }
    }
}
