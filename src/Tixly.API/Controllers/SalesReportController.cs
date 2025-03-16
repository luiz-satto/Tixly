using BuildingBlocks.Pagination;
using Microsoft.AspNetCore.Mvc;
using Tixly.Application.SalesReport.ExportSalesReport;
using Tixly.Application.SalesReport.GetSalesReport;
using Tixly.Domain.Dtos;

namespace Tixly.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public sealed class SalesReportController(
        IGetSalesReportUseCase getSalesReportUseCase,
        IExportSalesReport exportSalesReport
    ) : ControllerBase
    {
        [HttpGet(Name = "GetSalesReportByEvent")]
        [ProducesResponseType(typeof(PaginatedResult<SalesReportDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSalesReportByEventAsync([FromQuery] PaginationRequest request, CancellationToken cancellation)
        {
            var result = await getSalesReportUseCase.GetSalesReportByEventAsync(request, cancellation);
            return result;
        }

        [HttpGet(Name = "ExportSalesReportByEvent")]
        [ProducesResponseType(typeof(PaginatedResult<SalesReportDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExportSalesReportByEventAsync(CancellationToken cancellation)
        {
            var result = await exportSalesReport.ExportSalesReportByEventAsync(cancellation);
            return result;
        }
    }
}
