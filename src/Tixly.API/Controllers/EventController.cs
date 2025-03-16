using BuildingBlocks.Pagination;
using Microsoft.AspNetCore.Mvc;
using Tixly.Application.Events.CreateEvent;
using Tixly.Application.Events.DeleteEvent;
using Tixly.Application.Events.GetEvent;
using Tixly.Application.Events.UpdateEvent;
using Tixly.Domain.Dtos;

namespace Tixly.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public sealed class EventController(
        IGetEventUseCase getEventUseCase,
        ICreateEventUseCase createEventUseCase,
        IUpdateEventUseCase updateEventUseCase,
        IDeleteEventUseCase deleteEventUseCase
    ) : ControllerBase
    {
        [HttpGet(Name = "GetEvent")]
        [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEventAsync([FromQuery] GetEventRequest request, CancellationToken cancellation)
        {
            var result = await getEventUseCase.GetEventByIdAsync(request, cancellation);
            return result;
        }

        [HttpGet(Name = "GetEventList")]
        [ProducesResponseType(typeof(PaginatedResult<EventDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEventListAsync([FromQuery] PaginationRequest request, CancellationToken cancellation)
        {
            var result = await getEventUseCase.GetEventListAsync(request, cancellation);
            return result;
        }

        [HttpPost(Name = "CreateEvent")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEventAsync(CreateEventRequest request, CancellationToken cancellation)
        {
            var result = await createEventUseCase.CreateAsync(request, cancellation);
            return result;
        }

        [HttpPatch(Name = "UpdateEvent")]
        [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEventAsync(UpdateEventRequest request, CancellationToken cancellation)
        {
            var result = await updateEventUseCase.UpdateEventAsync(request, cancellation);
            return result;
        }

        [HttpDelete(Name = "DeleteEvent")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEventAsync([FromQuery] DeleteEventRequest request, CancellationToken cancellation)
        {
            var result = await deleteEventUseCase.DeleteEventAsync(request, cancellation);
            return result;
        }
    }
}
