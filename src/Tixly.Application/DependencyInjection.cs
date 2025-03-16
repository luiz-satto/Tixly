using Microsoft.Extensions.DependencyInjection;
using Tixly.Application.Events.CreateEvent;
using Tixly.Application.Events.DeleteEvent;
using Tixly.Application.Events.GetEvent;
using Tixly.Application.Events.UpdateEvent;
using Tixly.Application.SalesReport.ExportSalesReport;
using Tixly.Application.SalesReport.GetSalesReport;
using Tixly.Application.Tickets.CreateTicket;
using Tixly.Application.Tickets.DeleteTicket;
using Tixly.Application.Tickets.GetTicket;
using Tixly.Application.Tickets.UpdateTicket;

namespace Tixly.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IGetEventUseCase, GetEventUseCase>();
            services.AddScoped<ICreateEventUseCase, CreateEventUseCase>();
            services.AddScoped<IUpdateEventUseCase, UpdateEventUseCase>();
            services.AddScoped<IDeleteEventUseCase, DeleteEventUseCase>();

            services.AddScoped<IGetTicketUseCase, GetTicketUseCase>();
            services.AddScoped<ICreateTicketUseCase, CreateTicketUseCase>();
            services.AddScoped<IUpdateTicketUseCase, UpdateTicketUseCase>();
            services.AddScoped<IDeleteTicketUseCase, DeleteTicketUseCase>();

            services.AddScoped<IGetSalesReportUseCase, GetSalesReportUseCase>();
            services.AddScoped<IExportSalesReport, ExportSalesReport>();

            return services;
        }
    }
}
