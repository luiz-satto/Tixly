using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using Tixly.Domain.Dtos;
using Tixly.Domain.Exceptions;
using Tixly.Domain.Extensions;
using Tixly.Infrastructure.Models;
using Tixly.Infrastructure.Repositories.Event;

namespace Tixly.Domain.Commands.Events.UpdateEvent
{
    public class UpdateEventHandler(
        IEventRepository eventRepository
    ) : ICommandHandler<UpdateEventCommand, UpdateEventResult>
    {
        public async Task<UpdateEventResult> Handle(UpdateEventCommand command, CancellationToken cancellationToken)
        {
            var result = await UpdateEventCommandValidator(command, cancellationToken);
            if (result.IsFailure)
            {
                return new UpdateEventResult(Result.Failure<EventDto>(result.Error));
            }

            var updatedEvent = await eventRepository.UpdateAsync(result.Value, cancellationToken);
            return new UpdateEventResult(Result.Success(updatedEvent.ToEventDto()));
        }

        private async Task<Result<Event>> UpdateEventCommandValidator(UpdateEventCommand command, CancellationToken cancellationToken)
        {
            Error error = ValidateAsync(command, cancellationToken);
            if (error != null)
            {
                return Result.Failure<Event>(error);
            }

            Event @event = await eventRepository.GetAsync(command.Id, cancellationToken);
            if (@event == null)
            {
                return Result.Failure<Event>(new("Error.NotFound", new EventNotFoundException(command.Id).Message));
            }

            @event.Name = command.Name;
            @event.Venue = command.Venue;
            @event.Description = command.Description;
            @event.Date = command.Date;
            @event.TotalCapacity = command.TotalCapacity;
            @event.AvailableTickets = command.AvailableTickets;
            return Result.Success(@event);
        }

        private static Error ValidateAsync(UpdateEventCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.Name))
            {
                return new("Error.BadRequest", "Invalid Event Name: The event name is required.");
            }

            if (string.IsNullOrEmpty(command.Name))
            {
                return new("Error.BadRequest", "Invalid Event Name: The event name is required.");
            }

            if (string.IsNullOrEmpty(command.Venue))
            {
                return new("Error.BadRequest", "Invalid Event Venue: The venue is required.");
            }

            if (command.TotalCapacity <= 0)
            {
                return new("Error.BadRequest", "Invalid Total Capacity: The total capacity must be greater than 0. Please enter a valid amount.");
            }

            if (command.AvailableTickets <= 0)
            {
                return new("Error.BadRequest", "Invalid Available Tickets: The available tickets must be greater than 0. Please enter a valid amount.");
            }

            return null!;
        }
    }
}
