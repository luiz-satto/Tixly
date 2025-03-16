using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tixly.Domain.Commands.Events.DeleteEvent;

namespace Tixly.Application.Events.DeleteEvent
{
    public class DeleteEventUseCase(
        ISender sender,
        ILogger<DeleteEventUseCase> logger
    ) : ControllerBase, IDeleteEventUseCase
    {
        public async Task<IActionResult> DeleteEventAsync(DeleteEventRequest request, CancellationToken cancellationToken)
        {
            DeleteEventCommand command = new(request.Id);
            DeleteEventResult result = await sender.Send(command);

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

            logger.LogInformation($"Event : [{result.CommandResult.Value}] Deleted Successfuly!");
            return Ok(result.CommandResult.Value);
        }
    }
}
