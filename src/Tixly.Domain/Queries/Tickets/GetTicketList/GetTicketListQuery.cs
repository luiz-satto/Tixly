using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Tixly.Domain.Dtos;

namespace Tixly.Domain.Queries.Tickets.GetTicketList
{
    public record GetTicketListQuery(PaginationRequest PaginationRequest, Guid? EventId = null) : IQuery<GetTicketListResult>;
    public record GetTicketListResult(Result<PaginatedResult<TicketDto>> CommandResult);
}
