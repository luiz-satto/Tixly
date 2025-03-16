namespace Tixly.Domain.Dtos
{
    public record PricingTierDto(
        Guid Id,
        string Name,
        IEnumerable<string> Benefits,
        decimal Price,
        int Capacity,
        Guid EventId,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
