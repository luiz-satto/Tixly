using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tixly.Domain.Commands.Tickets.CreateTicket;

namespace Tixly.Application.Tickets.CreateTicket
{
    public class CreateTicketUseCase(
        ISender sender,
        ILogger<CreateTicketUseCase> logger
    ) : ControllerBase, ICreateTicketUseCase
    {
        public async Task<IActionResult> CreateAsync(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            CreateTicketCommand command = new(request.Price, request.Status, request.EventId, request.UserId);
            CreateTicketResult result = await sender.Send(command, cancellationToken);

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

            logger.LogInformation($"New Ticket : [{result.CommandResult.Value}] Created Successfuly!");
            return Ok(result.CommandResult.Value);
        }
    }
}
