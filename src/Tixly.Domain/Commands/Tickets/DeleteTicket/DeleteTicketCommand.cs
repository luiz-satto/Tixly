using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;

namespace Tixly.Domain.Commands.Tickets.DeleteTicket
{
    public record DeleteTicketCommand(Guid Id) : ICommand<DeleteTicketResult>;
    public record DeleteTicketResult(Result<bool> CommandResult);
}
