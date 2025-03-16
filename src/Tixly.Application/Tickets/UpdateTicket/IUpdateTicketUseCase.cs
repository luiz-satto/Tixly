using Microsoft.AspNetCore.Mvc;
using Tixly.Domain.Dtos;

namespace Tixly.Application.Tickets.UpdateTicket
{
    public record UpdateTicketResponse(TicketDto TicketDto);
    public record UpdateTicketRequest(
        Guid Id,
        decimal Price,
        string Status
    );

    public interface IUpdateTicketUseCase
    {
        Task<IActionResult> UpdateTicketAsync(UpdateTicketRequest request, CancellationToken cancellationToken);
    }
}
