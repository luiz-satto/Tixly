using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Tixly.Domain.Dtos;

namespace Tixly.Domain.Queries.SalesReport.GetSalesReportByEvent
{
    public record GetSalesReportByEventQuery(PaginationRequest PaginationRequest) : IQuery<GetSalesReportByEventResult>;
    public record GetSalesReportByEventResult(PaginatedResult<SalesReportDto> SalesReportsDto);
}
