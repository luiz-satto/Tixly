namespace Tixly.Domain.Dtos
{
    public record SalesReportDto(
        string EventName,
        int AvailableTickets,
        int TotalCapacity,
        int TotalSold,
        decimal TotalRevenue,
        DateTime EventDate,
        IEnumerable<TicketDto> TicketDtos
    );
}
