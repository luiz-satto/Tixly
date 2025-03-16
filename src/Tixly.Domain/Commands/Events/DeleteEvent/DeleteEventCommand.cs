using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;

namespace Tixly.Domain.Commands.Events.DeleteEvent
{
    public record DeleteEventCommand(Guid Id) : ICommand<DeleteEventResult>;
    public record DeleteEventResult(Result<bool> CommandResult);
}
