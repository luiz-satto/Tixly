using Microsoft.AspNetCore.Mvc;

namespace Tixly.Application.Tickets.CreateTicket
{
    public record CreateTicketResponse(Guid Id);
    public record CreateTicketRequest(
        decimal Price,
        string Status,
        Guid EventId,
        Guid UserId
    );

    public interface ICreateTicketUseCase
    {
        Task<IActionResult> CreateAsync(CreateTicketRequest request, CancellationToken cancellationToken);
    }
}
