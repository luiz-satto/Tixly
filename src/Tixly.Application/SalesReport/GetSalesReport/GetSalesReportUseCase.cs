using BuildingBlocks.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tixly.Domain.Queries.SalesReport.GetSalesReportByEvent;

namespace Tixly.Application.SalesReport.GetSalesReport
{
    public class GetSalesReportUseCase(
        ISender sender,
        ILogger<GetSalesReportUseCase> logger
    ) : ControllerBase, IGetSalesReportUseCase
    {
        public async Task<IActionResult> GetSalesReportByEventAsync(PaginationRequest request, CancellationToken cancellationToken)
        {
            GetSalesReportByEventResult result = await sender.Send(new GetSalesReportByEventQuery(request), cancellationToken);
            logger.LogInformation($"SalesReports Retrived Successfuly!");
            return Ok(result.SalesReportsDto);
        }
    }
}
