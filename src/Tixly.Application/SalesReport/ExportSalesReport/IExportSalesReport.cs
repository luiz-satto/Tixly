using Microsoft.AspNetCore.Mvc;

namespace Tixly.Application.SalesReport.ExportSalesReport
{
    public interface IExportSalesReport
    {
        Task<IActionResult> ExportSalesReportByEventAsync(CancellationToken cancellationToken);
    }
}
