using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using Tixly.Domain.Exceptions;
using Tixly.Infrastructure.Models;
using Tixly.Infrastructure.Repositories.Event;

namespace Tixly.Domain.Commands.Events.DeleteEvent
{
    public class DeleteEventHandler(IEventRepository eventRepository)
        : ICommandHandler<DeleteEventCommand, DeleteEventResult>
    {
        public async Task<DeleteEventResult> Handle(DeleteEventCommand command, CancellationToken cancellationToken)
        {
            Event @event = await eventRepository.GetAsync(command.Id, cancellationToken);
            if (@event == null)
            {
                Error error = new("Error.NotFound", new EventNotFoundException(command.Id).Message);
                return new DeleteEventResult(Result.Failure<bool>(error));
            }

            bool isDeleted = await eventRepository.DeleteAsync(command.Id, cancellationToken);
            return new DeleteEventResult(Result.Success(isDeleted));
        }
    }
}
