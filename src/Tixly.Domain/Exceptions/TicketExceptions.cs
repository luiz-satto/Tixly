using BuildingBlocks.Exceptions;

namespace Tixly.Domain.Exceptions
{
    public class TicketNotFoundException(Guid id) : NotFoundException("Ticket", id);
    public class TicketAlreadyExistsException(Guid id) : AlreadyExistsException("Ticket", id);
}
