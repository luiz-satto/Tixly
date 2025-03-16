using Microsoft.AspNetCore.Mvc;

namespace Tixly.Application.Events.CreateEvent
{
    public record CreateEventResponse(Guid Id);
    public record CreateEventRequest(
        string Name,
        string Venue,
        string Description,
        DateTime Date,
        int TotalCapacity,
        int AvailableTickets,
        Guid[] PricingTiers
    );

    public interface ICreateEventUseCase
    {
        Task<IActionResult> CreateAsync(CreateEventRequest request, CancellationToken cancellationToken);
    }
}
