

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extentions
{
    public static class DatabaseExtentions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();
            await SeedAsync(context);
        }
        private static async Task SeedAsync(ApplicationDbContext context)
        {
            await seedCustomerAsync(context);
            await seedProductAsync(context);
            await seedOrderWithItemsAsync(context);
        }

        private static async Task seedCustomerAsync(ApplicationDbContext context)
        {
            if (!await context.customers.AnyAsync())
            {
                context.customers.AddRange(InitialData.Customers);
                await context.SaveChangesAsync();
            }
        }
        private static async Task seedProductAsync(ApplicationDbContext context)
        {
            if (!await context.products.AnyAsync())
            {
                context.products.AddRange(InitialData.Products);
                await context.SaveChangesAsync();
            }
        }
        private static async Task seedOrderWithItemsAsync(ApplicationDbContext context)
        {
            if (!await context.orders.AnyAsync())
            {
                context.orders.AddRange(InitialData.Orders);
                await context.SaveChangesAsync();
            }
        }

    }
}