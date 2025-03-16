using BuildingBlocks.Pagination;
using ClosedXML.Excel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tixly.Application.SalesReport.GetSalesReport;
using Tixly.Domain.Queries.SalesReport.GetSalesReportByEvent;

namespace Tixly.Application.SalesReport.ExportSalesReport
{
    public class ExportSalesReport(
        ISender sender,
        ILogger<GetSalesReportUseCase> logger
    ) : ControllerBase, IExportSalesReport
    {
        public async Task<IActionResult> ExportSalesReportByEventAsync(CancellationToken cancellationToken)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sales Report");

            // Fetch sales report data
            var result = await sender.Send(new GetSalesReportByEventQuery(new PaginationRequest()), cancellationToken);
            var report = result.SalesReportsDto.Data;

            // Add data rows
            AddDataRows(worksheet, report);

            // Adjust column width for better readability
            worksheet.Columns().AdjustToContents();

            // Generate file
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            logger.LogInformation("Sales Reports exported successfully with {RowCount} rows.", report.Count());
            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "SalesReport.xlsx");
        }

        /// <summary>
        /// Adds header row with styles.
        /// </summary>
        private static void AddHeaderRow(IXLWorksheet worksheet, string[] headers, int row)
        {
            for (int col = 0; col < headers.Length; col++)
            {
                worksheet.Cell(row, col + 1).Value = headers[col];
            }

            var headerRange = worksheet.Range(row, 1, row, headers.Length);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
        }

        /// <summary>
        /// Populates the worksheet with sales report data.
        /// </summary>
        private static void AddDataRows(IXLWorksheet worksheet, IEnumerable<Domain.Dtos.SalesReportDto> report)
        {
            // Define headers
            string[] headers = ["Event Name", "Total Tickets Sold", "Total Revenue ($)", "Total Capacity", "Available Tickets", "Event Date"];

            int row = 1;
            foreach (var item in report)
            {
                // Add header row
                AddHeaderRow(worksheet, headers, row);
                row++;

                worksheet.Cell(row, 1).Value = item.EventName;
                worksheet.Cell(row, 2).Value = item.TotalSold;
                worksheet.Cell(row, 3).Value = item.TotalRevenue;
                worksheet.Cell(row, 4).Value = item.TotalCapacity;
                worksheet.Cell(row, 5).Value = item.AvailableTickets;
                worksheet.Cell(row, 6).Value = item.EventDate.ToString("yyyy-MM-dd HH:mm:ss");

                // Add ticket details
                if (item.TicketDtos != null)
                {
                    foreach (var ticket in item.TicketDtos)
                    {
                        row++;
                        worksheet.Cell(row, 1).Value = ticket.Id.ToString();
                        worksheet.Cell(row, 2).Value = $"{ticket.User?.FirstName} {ticket.User?.LastName}";
                        worksheet.Cell(row, 3).Value = ticket.Price;
                        worksheet.Cell(row, 4).Value = ticket.Status;
                    }
                }

                row++; // Move to next event row
            }
        }
    }
}
