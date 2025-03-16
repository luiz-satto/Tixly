using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using Tixly.Domain.Exceptions;
using Tixly.Infrastructure.Models;
using Tixly.Infrastructure.Repositories.Event;
using Tixly.Infrastructure.Repositories.PricingTier;

namespace Tixly.Domain.Commands.Events.CreateEvent
{
    public class CreateEventHandler(
        IEventRepository eventRepository,
        IPricingTierRepository pricingTierRepository
    ) : ICommandHandler<CreateEventCommand, CreateEventResult>
    {
        public async Task<CreateEventResult> Handle(CreateEventCommand command, CancellationToken cancellationToken)
        {
            var result = await CreateEventCommandValidator(command, cancellationToken);
            if (result.IsFailure)
            {
                return new CreateEventResult(Result.Failure<Guid>(result.Error));
            }

            Guid eventId = await eventRepository.InsertAsync(result.Value, cancellationToken);
            return new CreateEventResult(Result.Success(eventId));
        }

        private async Task<Result<Event>> CreateEventCommandValidator(CreateEventCommand command, CancellationToken cancellationToken)
        {
            Error error = await ValidateAsync(command, cancellationToken);
            if (error != null)
            {
                return Result.Failure<Event>(error);
            }

            var pricingTiers = command.PricingTiers.Select(priceTierId => new PricingTier() { Id = priceTierId }).ToList();
            var @event = new Event()
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Venue = command.Venue,
                Description = command.Description,
                Date = command.Date,
                TotalCapacity = command.TotalCapacity,
                AvailableTickets = command.AvailableTickets,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PricingTiers = pricingTiers,
            };

            return Result.Success(@event);
        }

        private async Task<Error> ValidateAsync(CreateEventCommand command, CancellationToken cancellationToken)
        {
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

            return await ValidatePricingTier(command.PricingTiers, cancellationToken);
        }

        private async Task<Error> ValidatePricingTier(Guid[] pricingTiers, CancellationToken cancellationToken)
        {
            foreach (var priceTierId in pricingTiers)
            {
                var priceTier = await pricingTierRepository.GetAsync(priceTierId, cancellationToken);
                if (priceTier == null)
                {
                    return new("Error.NotFound", new PriceTierNotFoundException(priceTierId).Message);
                }
            }

            return null!;
        }
    }
}
