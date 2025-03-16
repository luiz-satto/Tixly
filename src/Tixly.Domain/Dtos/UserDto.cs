namespace Tixly.Domain.Dtos
{
    public record UserDto(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        int Role,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
