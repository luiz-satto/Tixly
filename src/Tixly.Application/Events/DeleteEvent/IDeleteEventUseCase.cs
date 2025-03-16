using Microsoft.AspNetCore.Mvc;

namespace Tixly.Application.Events.DeleteEvent
{
    public record DeleteEventRequest(Guid Id);
    public record DeleteEventResponse(bool IsDeleted);
    public interface IDeleteEventUseCase
    {
        Task<IActionResult> DeleteEventAsync(DeleteEventRequest request, CancellationToken cancellationToken);
    }
}
