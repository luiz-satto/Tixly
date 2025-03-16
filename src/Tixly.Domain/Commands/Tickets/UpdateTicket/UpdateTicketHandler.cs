using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using BuildingBlocks.Enums;
using Tixly.Domain.Dtos;
using Tixly.Domain.Exceptions;
using Tixly.Domain.Extensions;
using Tixly.Infrastructure.Models;
using Tixly.Infrastructure.Repositories.Ticket;

namespace Tixly.Domain.Commands.Tickets.UpdateTicket
{
    public class UpdateTicketHandler(ITicketRepository ticketRepository)
        : ICommandHandler<UpdateTicketCommand, UpdateTicketResult>
    {
        public async Task<UpdateTicketResult> Handle(UpdateTicketCommand command, CancellationToken cancellationToken)
        {
            var result = await UpdateTicketCommandValidatorAsync(command, cancellationToken);
            if (result.IsFailure)
            {
                return new UpdateTicketResult(Result.Failure<TicketDto>(result.Error));
            }

            var ticket = result.Value;
            ticket.Price = command.Price;
            ticket.Status = command.Status;
            ticket.UpdatedAt = DateTime.UtcNow;

            var updatedTicket = await ticketRepository.UpdateAsync(ticket, cancellationToken);
            return new UpdateTicketResult(Result.Success(updatedTicket.ToTicketDto()));
        }

        private async Task<Result<Ticket>> UpdateTicketCommandValidatorAsync(UpdateTicketCommand command, CancellationToken cancellationToken)
        {
            Ticket ticket = await ticketRepository.GetAsync(command.Id, cancellationToken);
            Error error = Validate(command, ticket);
            if (error != null)
            {
                return Result.Failure<Ticket>(error);
            }

            return Result.Success(ticket);
        }

        private static Error Validate(UpdateTicketCommand command, Ticket ticket)
        {
            if (ticket == null)
            {
                return new("Error.NotFound", new TicketNotFoundException(command.Id).Message);
            }

            if (command.Status != TicketStatus.Booked && command.Status != TicketStatus.Canceled)
            {
                return new("Error.BadRequest", $"Invalid Ticket Status: {command.Status}. Please enter either '{TicketStatus.Booked}' or '{TicketStatus.Canceled}'.");
            }

            if (command.Price <= 0)
            {
                return new("Error.BadRequest", "Invalid Ticket Price: The price must be greater than 0. Please enter a valid amount.");
            }

            return null!;
        }
    }
}
