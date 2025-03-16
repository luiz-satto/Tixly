using BuildingBlocks.Pagination;
using Microsoft.AspNetCore.Mvc;
using Tixly.Domain.Dtos;

namespace Tixly.Application.SalesReport.GetSalesReport
{
    public record GetSalesReportResponse(SalesReportDto SalesReportDto);
    public record GetSalesReportsResponse(PaginatedResult<SalesReportDto> SalesReportsDto);

    public interface IGetSalesReportUseCase
    {
        Task<IActionResult> GetSalesReportByEventAsync(PaginationRequest request, CancellationToken cancellationToken);
    }
}
