using Tixly.Domain.Dtos;
using Tixly.Infrastructure.Models;

namespace Tixly.Domain.Extensions
{
    public static class EventExtension
    {
        public static IEnumerable<EventDto> ToEventDtoList(this IEnumerable<Event> events)
            => events.Select(ToEventDto);

        public static EventDto ToEventDto(this Event @event)
        {
            return new EventDto(
                @event.Id,
                @event.Name,
                @event.Venue,
                @event.Description,
                @event.Date,
                @event.TotalCapacity,
                @event.AvailableTickets,
                @event.CreatedAt,
                @event.UpdatedAt,
                @event.PricingTiers
            );
        }
    }
}
