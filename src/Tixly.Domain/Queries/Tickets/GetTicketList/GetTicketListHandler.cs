using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Tixly.Domain.Dtos;
using Tixly.Domain.Extensions;
using Tixly.Infrastructure.Repositories.Ticket;

namespace Tixly.Domain.Queries.Tickets.GetTicketList
{
    public class GetTicketListHandler(ITicketRepository ticketRepository)
        : IQueryHandler<GetTicketListQuery, GetTicketListResult>
    {
        public async Task<GetTicketListResult> Handle(GetTicketListQuery query, CancellationToken cancellationToken)
        {
            int pageIndex = query.PaginationRequest.PageIndex;
            int pageSize = query.PaginationRequest.PageSize;
            var tickets = await ticketRepository.GetAsync(pageIndex, pageSize, cancellationToken);
            IEnumerable<TicketDto> ticketsDto = tickets != null ? tickets.ToTicketDtoList() : default!;
            PaginatedResult<TicketDto> result = new(pageIndex, pageSize, ticketsDto.Count(), ticketsDto);
            return new GetTicketListResult(Result.Success(result));
        }
    }
}
