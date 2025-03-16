using BuildingBlocks.Pagination;
using Microsoft.AspNetCore.Mvc;
using Tixly.Domain.Dtos;

namespace Tixly.Application.Tickets.GetTicket
{
    public record GetTicketsRequest();
    public record GetTicketRequest(Guid Id);

    public record GetTicketResponse(TicketDto TicketDto);
    public record GetTicketsResponse(PaginatedResult<TicketDto> TicketsDto);

    public interface IGetTicketUseCase
    {
        Task<IActionResult> GetTicketByIdAsync(GetTicketRequest request, CancellationToken cancellationToken);
        Task<IActionResult> GetTicketListAsync(PaginationRequest request, CancellationToken cancellationToken);
    }
}
