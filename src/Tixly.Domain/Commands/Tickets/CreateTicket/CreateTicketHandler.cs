using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using BuildingBlocks.Enums;
using Tixly.Domain.Exceptions;
using Tixly.Infrastructure.Models;
using Tixly.Infrastructure.Repositories.Event;
using Tixly.Infrastructure.Repositories.Ticket;
using Tixly.Infrastructure.Repositories.User;

namespace Tixly.Domain.Commands.Tickets.CreateTicket
{
    public class CreateTicketHandler(
        ITicketRepository ticketRepository,
        IEventRepository eventRepository,
        IUserRepository userRepository)
        : ICommandHandler<CreateTicketCommand, CreateTicketResult>
    {
        public async Task<CreateTicketResult> Handle(CreateTicketCommand command, CancellationToken cancellationToken)
        {
            var error = await CreateTicketCommandValidatorAsync(command, cancellationToken);
            if (error != null)
            {
                return new CreateTicketResult(Result.Failure<Guid>(error));
            }

            Ticket ticket = new()
            {
                Id = Guid.NewGuid(),
                Price = command.Price,
                Status = command.Status,
                EventId = command.EventId,
                UserId = command.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            Guid ticketId = await ticketRepository.InsertAsync(ticket, cancellationToken);
            return new CreateTicketResult(Result.Success(ticketId));
        }

        private async Task<Error?> CreateTicketCommandValidatorAsync(CreateTicketCommand command, CancellationToken cancellationToken)
        {
            if (command.Status != TicketStatus.Booked && command.Status != TicketStatus.Canceled)
            {
                return new("Error.BadRequest", $"Invalid Ticket Status: {command.Status}. Please enter either '{TicketStatus.Booked}' or '{TicketStatus.Canceled}'.");
            }

            if (command.Price <= 0)
            {
                return new("Error.BadRequest", "Invalid Ticket Price: The price must be greater than 0. Please enter a valid amount.");
            }

            var @event = await eventRepository.GetAsync(command.EventId, cancellationToken);
            if (@event == null)
            {
                return new("Error.NotFound", new EventNotFoundException(command.EventId).Message);
            }

            var user = await userRepository.GetAsync(command.UserId, cancellationToken);
            if (user == null)
            {
                return new("Error.NotFound", new UserNotFoundException(command.UserId).Message);
            }

            return null;
        }
    }
}
