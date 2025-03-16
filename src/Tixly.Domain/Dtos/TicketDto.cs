using Tixly.Infrastructure.Models;

namespace Tixly.Domain.Dtos
{
    public record TicketDto(
        Guid Id,
        decimal Price,
        string Status,
        Event? Event,
        User? User
    );
}
