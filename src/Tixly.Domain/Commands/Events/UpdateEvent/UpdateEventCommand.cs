using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using Tixly.Domain.Dtos;

namespace Tixly.Domain.Commands.Events.UpdateEvent
{
    public record UpdateEventCommand(Guid Id, string Name, string Venue, string Description, DateTime Date, int TotalCapacity, int AvailableTickets) : ICommand<UpdateEventResult>;
    public record UpdateEventResult(Result<EventDto> CommandResult);
}
