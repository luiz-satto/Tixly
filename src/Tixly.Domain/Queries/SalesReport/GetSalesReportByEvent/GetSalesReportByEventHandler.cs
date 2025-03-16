using BuildingBlocks.CQRS;
using BuildingBlocks.Enums;
using Tixly.Domain.Dtos;
using Tixly.Domain.Extensions;
using Tixly.Infrastructure.Repositories.Ticket;

namespace Tixly.Domain.Queries.SalesReport.GetSalesReportByEvent
{
    public class GetSalesReportByEventHandler(ITicketRepository ticketRepository)
        : IQueryHandler<GetSalesReportByEventQuery, GetSalesReportByEventResult>
    {
        public async Task<GetSalesReportByEventResult> Handle(GetSalesReportByEventQuery query, CancellationToken cancellationToken)
        {
            int pageIndex = query.PaginationRequest.PageIndex;
            int pageSize = query.PaginationRequest.PageSize;
            var result = await ticketRepository.GetAsync(pageIndex, pageSize, cancellationToken);
            if (result == null)
            {
                return default!;
            }

            IEnumerable<TicketDto> tickets = result.Where(x => x.Status != TicketStatus.Canceled).ToTicketDtoList();
            var salesReport = tickets
                .GroupBy(ticket => ticket.Event?.Id)
                .Select(group => GetSalesReportDto(group, tickets))
                .ToList();

            if (salesReport == null)
            {
                return default!;
            }

            return new GetSalesReportByEventResult(new(
                query.PaginationRequest.PageIndex,
                query.PaginationRequest.PageSize,
                salesReport.Count,
                salesReport)
            );
        }

        private static SalesReportDto GetSalesReportDto(IGrouping<Guid?, TicketDto> group, IEnumerable<TicketDto> tickets)
        {
            if (group.Key == null)
            {
                return default!;
            }

            var @event = tickets.Select(x => x.Event).FirstOrDefault(e => e?.Id == group.Key);
            if (@event == null)
            {
                return default!;
            }

            var ticketsByEvent = tickets.Where(x => x.Event?.Id == group.Key);
            return new SalesReportDto(
                @event.Name,
                @event.AvailableTickets,
                @event.TotalCapacity,
                group.Count(),
                group.Sum(t => t.Price),
                @event.Date,
                ticketsByEvent
            );
        }
    }
}
