using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.Enums;

namespace Tixly.Infrastructure.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public int Role { get; set; } = (int)UserRole.CUSTOMER;
    }
}
