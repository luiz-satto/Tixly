using BuildingBlocks.Exceptions;

namespace Tixly.Domain.Exceptions
{
    public class PriceTierNotFoundException(Guid id) : NotFoundException("Price Tier", id);
    public class PriceTierAlreadyExistsException(Guid id) : AlreadyExistsException("Price Tier", id);
}
