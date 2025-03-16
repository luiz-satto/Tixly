using BuildingBlocks.Exceptions;

namespace Tixly.Domain.Exceptions
{
    public class EventNotFoundException(Guid id) : NotFoundException("Event", id);
    public class EventAlreadyExistsException(Guid id) : AlreadyExistsException("Event", id);
}
