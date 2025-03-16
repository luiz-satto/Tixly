using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tixly.Domain.Dtos;
using Tixly.Domain.Exceptions;
using Tixly.Domain.Queries.Events.GetEventById;
using Tixly.Domain.Queries.Events.GetEventList;

namespace Tixly.Application.Events.GetEvent
{
    public class GetEventUseCase(
        ISender sender,
        ILogger<GetEventUseCase> logger
    ) : ControllerBase, IGetEventUseCase
    {
        public async Task<IActionResult> GetEventByIdAsync(GetEventRequest request, CancellationToken cancellationToken)
        {
            GetEventByIdQuery query = new(request.Id);
            GetEventByIdResult result = await sender.Send(query, cancellationToken);

            if (result.CommandResult.IsFailure)
            {
                var error = result.CommandResult.Error;
                if (error.Code == Error.NotFound)
                {
                    logger.LogInformation($"Event : [{request.Id}] Not Found.");
                    return NotFound(new EventNotFoundException(request.Id));
                }

                return BadRequest(new BadRequestException(error.Message));
            }

            logger.LogInformation($"Event : [{request.Id}] Retrived Successfuly!");
            return Ok(result.CommandResult.Value);
        }

        public async Task<IActionResult> GetEventListAsync(PaginationRequest request, CancellationToken cancellationToken)
        {
            GetEventListResult result = await sender.Send(new GetEventListQuery(request), cancellationToken);
            logger.LogInformation($"Events Retrived Successfuly!");
            return Ok(result.CommandResult.Value);
        }
    }
}
