using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tixly.Domain.Commands.Tickets.UpdateTicket;

namespace Tixly.Application.Tickets.UpdateTicket
{
    public class UpdateTicketUseCase(
        ISender sender,
        ILogger<UpdateTicketUseCase> logger
    ) : ControllerBase, IUpdateTicketUseCase
    {
        public async Task<IActionResult> UpdateTicketAsync(UpdateTicketRequest request, CancellationToken cancellationToken)
        {
            UpdateTicketCommand command = new(request.Id, request.Price, request.Status);
            UpdateTicketResult result = await sender.Send(command, cancellationToken);

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

            logger.LogInformation($"Ticket : [{result.CommandResult.Value}] Updated Successfuly!");
            return Ok(result.CommandResult.Value);
        }
    }
}
