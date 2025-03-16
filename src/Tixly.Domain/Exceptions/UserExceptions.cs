using BuildingBlocks.Exceptions;

namespace Tixly.Domain.Exceptions
{
    public class UserNotFoundException(Guid id) : NotFoundException("User", id);
    public class UserAlreadyExistsException(Guid id) : AlreadyExistsException("User", id);
}
