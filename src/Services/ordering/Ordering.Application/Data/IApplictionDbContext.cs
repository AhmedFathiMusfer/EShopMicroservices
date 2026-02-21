

using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;

namespace Ordering.Application.Data
{
    public interface IApplictionDbContext
    {
        DbSet<Customer> customers { get; }
        DbSet<Order> orders { get; }
        DbSet<Product> products { get; }
        DbSet<OrderItem> orderItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);


    }
}