using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;

namespace Tixly.Domain.Commands.Events.CreateEvent
{
    public record CreateEventCommand(
        string Name, 
        string Venue, 
        string Description, 
        DateTime Date, 
        int TotalCapacity, 
        int AvailableTickets,
        Guid[] PricingTiers
    ) : ICommand<CreateEventResult>;

    public record CreateEventResult(Result<Guid> CommandResult);
}
