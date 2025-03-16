using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using Tixly.Domain.Dtos;

namespace Tixly.Domain.Queries.Tickets.GetTicketById
{
    public record GetTicketByIdQuery(Guid Id) : IQuery<GetTicketByIdResult>;
    public record GetTicketByIdResult(Result<TicketDto> TicketDto);
}
