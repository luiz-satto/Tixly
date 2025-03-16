using Microsoft.AspNetCore.Mvc;

namespace Tixly.Application.Tickets.DeleteTicket
{
    public record DeleteTicketRequest(Guid Id);
    public record DeleteTicketResponse(bool IsDeleted);
    public interface IDeleteTicketUseCase
    {
        Task<IActionResult> DeleteTicketAsync(DeleteTicketRequest request, CancellationToken cancellationToken);
    }
}
