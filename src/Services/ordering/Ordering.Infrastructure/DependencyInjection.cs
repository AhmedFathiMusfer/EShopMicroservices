

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructreSevcies(this IServiceCollection service, IConfiguration configuration)
        {
            var connctionString = configuration.GetConnectionString("Database");
            service.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(connctionString));

            return service;
        }
    }
}