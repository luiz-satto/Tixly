using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tixly.Infrastructure.Data;
using Tixly.Infrastructure.Repositories.Event;
using Tixly.Infrastructure.Repositories.PricingTier;
using Tixly.Infrastructure.Repositories.Ticket;
using Tixly.Infrastructure.Repositories.User;

namespace Tixly.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IPricingTierRepository, PricingTierRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
            });

            return services;
        }
    }
}
