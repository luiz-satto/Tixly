using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tixly.Domain.Queries.Tickets.GetTicketById;
using Tixly.Domain.Queries.Tickets.GetTicketList;

namespace Tixly.Application.Tickets.GetTicket
{
    public class GetTicketUseCase(
        ISender sender,
        ILogger<GetTicketUseCase> logger
    ) : ControllerBase, IGetTicketUseCase
    {
        public async Task<IActionResult> GetTicketByIdAsync(GetTicketRequest request, CancellationToken cancellationToken)
        {
            GetTicketByIdQuery query = new(request.Id);
            GetTicketByIdResult result = await sender.Send(query, cancellationToken);

            if (result.TicketDto.IsFailure)
            {
                var error = result.TicketDto.Error;
                if (error.Code == Error.NotFound)
                {
                    logger.LogInformation(error.Message);
                    return NotFound(new NotFoundException(error.Message, request));
                }

                return BadRequest(new BadRequestException(error.Message));
            }

            logger.LogInformation($"Ticket : [{result.TicketDto.Value}] Retrived Successfuly!");
            return Ok(result.TicketDto.Value);
        }

        public async Task<IActionResult> GetTicketListAsync(PaginationRequest request, CancellationToken cancellationToken)
        {
            GetTicketListResult result = await sender.Send(new GetTicketListQuery(request), cancellationToken);
            logger.LogInformation($"Tickets Retrived Successfuly!");
            return Ok(result.CommandResult.Value);
        }
    }
}
