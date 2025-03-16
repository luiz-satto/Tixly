using Tixly.Domain.Dtos;
using Tixly.Infrastructure.Models;

namespace Tixly.Domain.Extensions
{
    public static class TicketExtension
    {
        public static IEnumerable<TicketDto> ToTicketDtoList(this IEnumerable<Ticket> tickets)
            => tickets.Select(ToTicketDto);

        public static TicketDto ToTicketDto(this Ticket ticket)
        {
            return new TicketDto(
                ticket.Id,
                ticket.Price,
                ticket.Status,
                ticket.Event,
                ticket.User
            );
        }
    }
}
