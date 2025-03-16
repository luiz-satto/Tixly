using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;

namespace Tixly.Domain.Commands.Tickets.CreateTicket
{
    public record CreateTicketCommand(decimal Price, string Status, Guid EventId, Guid UserId) : ICommand<CreateTicketResult>;
    public record CreateTicketResult(Result<Guid> CommandResult);
}
