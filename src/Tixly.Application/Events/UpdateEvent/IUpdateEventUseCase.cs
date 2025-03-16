using Microsoft.AspNetCore.Mvc;
using Tixly.Domain.Dtos;

namespace Tixly.Application.Events.UpdateEvent
{
    public record UpdateEventResponse(EventDto EventDto);
    public record UpdateEventRequest(
        Guid Id,
        string Name,
        string Venue,
        string Description,
        DateTime Date,
        int TotalCapacity,
        int AvailableTickets
    );

    public interface IUpdateEventUseCase
    {
        Task<IActionResult> UpdateEventAsync(UpdateEventRequest request, CancellationToken cancellationToken);
    }
}
