using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tixly.Domain.Commands.Tickets.DeleteTicket;

namespace Tixly.Application.Tickets.DeleteTicket
{
    public class DeleteTicketUseCase(
        ISender sender,
        ILogger<DeleteTicketUseCase> logger
    ) : ControllerBase, IDeleteTicketUseCase
    {
        public async Task<IActionResult> DeleteTicketAsync(DeleteTicketRequest request, CancellationToken cancellationToken)
        {
            DeleteTicketCommand command = new(request.Id);
            DeleteTicketResult result = await sender.Send(command);

            if (result.CommandResult.IsFailure)
            {
                var error = result.CommandResult.Error;
                if (error.Code == Error.NotFound)
                {
                    logger.LogInformation(error.Message);
                    return NotFound(new NotFoundException(error.Message, request));
                }

                return BadRequest(new BadRequestException(error.Message));
            }

            logger.LogInformation($"Ticket : [{result.CommandResult.Value}] Deleted Successfuly!");
            return Ok(result.CommandResult.Value);
        }
    }
}
