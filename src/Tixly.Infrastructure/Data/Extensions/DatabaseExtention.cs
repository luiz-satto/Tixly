using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tixly.Infrastructure.Data.Extensions
{
    public static class DatabaseExtention
    {
        public static async Task InitialiseDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();
            await SeedAsync(context);
        }

        private static async Task SeedAsync(AppDbContext context)
        {
            await SeedUserAsync(context);
            await SeedEventAsync(context);
            await SeedTicketAsync(context);
            await SeedPricingTierAsync(context);
        }

        private static async Task SeedUserAsync(AppDbContext context)
        {
            if (!await context.Users.AnyAsync())
            {
                await context.Users.AddRangeAsync(InitialData.Users);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedEventAsync(AppDbContext context)
        {
            if (!await context.Events.AnyAsync())
            {
                await context.Events.AddRangeAsync(InitialData.Events);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedTicketAsync(AppDbContext context)
        {
            if (!await context.Tickets.AnyAsync())
            {
                await context.Tickets.AddRangeAsync(InitialData.Tickets);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedPricingTierAsync(AppDbContext context)
        {
            if (!await context.PricingTiers.AnyAsync())
            {
                await context.PricingTiers.AddRangeAsync(InitialData.PricingTiers);
                await context.SaveChangesAsync();
            }
        }
    }
}
