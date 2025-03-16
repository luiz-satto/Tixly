using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using Tixly.Domain.Dtos;
using Tixly.Domain.Extensions;
using Tixly.Infrastructure.Models;
using Tixly.Infrastructure.Repositories.Ticket;

namespace Tixly.Domain.Queries.Tickets.GetTicketById
{
    public class GetTicketByIdHandler(ITicketRepository ticketRepository)
        : IQueryHandler<GetTicketByIdQuery, GetTicketByIdResult>
    {
        public async Task<GetTicketByIdResult> Handle(GetTicketByIdQuery query, CancellationToken cancellationToken)
        {
            Ticket ticket = await ticketRepository.GetAsync(query.Id, cancellationToken);
            if (ticket == null)
            {
                return new GetTicketByIdResult(Result.Failure<TicketDto>(Error.NotFound));
            }

            return new GetTicketByIdResult(Result.Success(ticket.ToTicketDto()));
        }
    }
}
