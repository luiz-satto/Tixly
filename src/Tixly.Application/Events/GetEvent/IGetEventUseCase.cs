using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.Pagination;
using Microsoft.AspNetCore.Mvc;
using Tixly.Domain.Dtos;

namespace Tixly.Application.Events.GetEvent
{
    public record GetEventsRequest();
    public record GetEventRequest(Guid Id);

    public record GetEventResponse(EventDto EventDto);
    public record GetEventsResponse(PaginatedResult<EventDto> EventsDto);

    public interface IGetEventUseCase
    {
        Task<IActionResult> GetEventByIdAsync(GetEventRequest request, CancellationToken cancellationToken);
        Task<IActionResult> GetEventListAsync(PaginationRequest request, CancellationToken cancellationToken);
    }
}
