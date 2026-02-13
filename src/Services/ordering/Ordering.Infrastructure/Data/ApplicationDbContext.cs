

using System.Dynamic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> customers => Set<Customer>();
        public DbSet<Product> products => Set<Product>();

        public DbSet<OrderItem> orderItems => Set<OrderItem>();
        public DbSet<Order> orders => Set<Order>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            new CustomerConiguration().Configure(modelBuilder.Entity<Customer>());
            base.OnModelCreating(modelBuilder);
        }

    }
    public class CustomerConiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        }
    }
}