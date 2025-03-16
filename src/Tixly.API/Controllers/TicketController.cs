using BuildingBlocks.Pagination;
using Microsoft.AspNetCore.Mvc;
using Tixly.Application.Tickets.CreateTicket;
using Tixly.Application.Tickets.DeleteTicket;
using Tixly.Application.Tickets.GetTicket;
using Tixly.Application.Tickets.UpdateTicket;
using Tixly.Domain.Dtos;

namespace Tixly.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public sealed class TicketController(
        IGetTicketUseCase getTicketUseCase,
        ICreateTicketUseCase createTicketUseCase,
        IUpdateTicketUseCase updateTicketUseCase,
        IDeleteTicketUseCase deleteTicketUseCase
    ) : ControllerBase
    {
        [HttpGet(Name = "GetTicket")]
        [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTicketAsync([FromQuery] GetTicketRequest request, CancellationToken cancellation)
        {
            var result = await getTicketUseCase.GetTicketByIdAsync(request, cancellation);
            return result;
        }

        [HttpGet(Name = "GetTicketList")]
        [ProducesResponseType(typeof(PaginatedResult<TicketDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTicketListAsync([FromQuery] PaginationRequest request, CancellationToken cancellation)
        {
            var result = await getTicketUseCase.GetTicketListAsync(request, cancellation);
            return result;
        }

        [HttpPost(Name = "CreateTicket")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTicketAsync(CreateTicketRequest request, CancellationToken cancellation)
        {
            var result = await createTicketUseCase.CreateAsync(request, cancellation);
            return result;
        }

        [HttpPatch(Name = "UpdateTicket")]
        [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTicketAsync(UpdateTicketRequest request, CancellationToken cancellation)
        {
            var result = await updateTicketUseCase.UpdateTicketAsync(request, cancellation);
            return result;
        }

        [HttpDelete(Name = "DeleteTicket")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTicketAsync([FromQuery] DeleteTicketRequest request, CancellationToken cancellation)
        {
            var result = await deleteTicketUseCase.DeleteTicketAsync(request, cancellation);
            return result;
        }
    }
}
