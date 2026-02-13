

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ComplexProperty(o => o.BillingAddress);
            builder.ComplexProperty(o => o.ShippingAddress);
            builder.ComplexProperty(o => o.Payment);
            builder.HasMany(o => o.OrderItems).WithOne().HasForeignKey(oi => oi.OrderId);
            builder.Property(o => o.Status).HasDefaultValue(OrderStatus.Draft).HasConversion(stauts => stauts.ToString(), dbstatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbstatus));



        }
    }
}