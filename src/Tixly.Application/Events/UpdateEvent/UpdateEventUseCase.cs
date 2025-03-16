using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tixly.Domain.Commands.Events.UpdateEvent;

namespace Tixly.Application.Events.UpdateEvent
{
    public class UpdateEventUseCase(
        ISender sender,
        ILogger<UpdateEventUseCase> logger
    ) : ControllerBase, IUpdateEventUseCase
    {
        public async Task<IActionResult> UpdateEventAsync(UpdateEventRequest request, CancellationToken cancellationToken)
        {
            UpdateEventCommand command = new(
                request.Id,
                request.Name,
                request.Venue,
                request.Description,
                request.Date,
                request.TotalCapacity,
                request.AvailableTickets
            );

            UpdateEventResult result = await sender.Send(command, cancellationToken);

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

            logger.LogInformation($"Event : [{result.CommandResult.Value}] Updated Successfuly!");
            return Ok(result.CommandResult.Value);
        }
    }
}
