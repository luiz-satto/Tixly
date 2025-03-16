using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tixly.Domain.Commands.Events.CreateEvent;

namespace Tixly.Application.Events.CreateEvent
{
    public class CreateEventUseCase(
        ISender sender,
        ILogger<CreateEventUseCase> logger
    ) : ControllerBase, ICreateEventUseCase
    {
        public async Task<IActionResult> CreateAsync(CreateEventRequest request, CancellationToken cancellationToken)
        {
            CreateEventCommand command = new(
                request.Name,
                request.Venue,
                request.Description,
                request.Date,
                request.TotalCapacity,
                request.AvailableTickets,
                request.PricingTiers
            );

            CreateEventResult result = await sender.Send(command, cancellationToken);

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

            logger.LogInformation($"New Event : [{result.CommandResult.Value}] Created Successfuly!");
            return Ok(result.CommandResult.Value);
        }
    }
}
