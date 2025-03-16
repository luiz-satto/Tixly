using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using Tixly.Domain.Exceptions;
using Tixly.Infrastructure.Models;
using Tixly.Infrastructure.Repositories.Ticket;

namespace Tixly.Domain.Commands.Tickets.DeleteTicket
{
    public class DeleteTicketHandler(ITicketRepository ticketRepository)
        : ICommandHandler<DeleteTicketCommand, DeleteTicketResult>
    {
        public async Task<DeleteTicketResult> Handle(DeleteTicketCommand command, CancellationToken cancellationToken)
        {
            Ticket ticket = await ticketRepository.GetAsync(command.Id, cancellationToken);
            if (ticket == null)
            {
                Error error = new("Error.NotFound", new TicketNotFoundException(command.Id).Message);
                return new DeleteTicketResult(Result.Failure<bool>(error));
            }

            bool isDeleted = await ticketRepository.DeleteAsync(command.Id, cancellationToken);
            return new DeleteTicketResult(Result.Success(isDeleted));
        }
    }
}
