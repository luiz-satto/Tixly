using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using Tixly.Domain.Dtos;

namespace Tixly.Domain.Commands.Tickets.UpdateTicket
{
    public record UpdateTicketCommand(Guid Id, decimal Price, string Status) : ICommand<UpdateTicketResult>;
    public record UpdateTicketResult(Result<TicketDto> CommandResult);
}
