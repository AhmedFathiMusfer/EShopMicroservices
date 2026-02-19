

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasConversion(OrderId => OrderId.Value, dbId => OrderId.Of(dbId));
            builder.HasOne<Customer>().WithMany().HasForeignKey(c => c.CustomerId).IsRequired();
            builder.HasMany(o => o.OrderItems).WithOne().HasForeignKey(oi => oi.OrderId);
            builder.ComplexProperty(o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(o => o.Value).HasColumnName(nameof(Order.OrderName)).HasMaxLength(100).IsRequired();
            });
            builder.ComplexProperty(o => o.ShippingAddress, addressBuilder =>
                       {
                           addressBuilder.Property(o => o.FirstName).HasMaxLength(50).IsRequired();
                           addressBuilder.Property(o => o.LastName).HasMaxLength(50).IsRequired();
                           addressBuilder.Property(o => o.EmailAddress).HasMaxLength(50).IsRequired();
                           addressBuilder.Property(o => o.AddressLine).HasMaxLength(180).IsRequired();
                           addressBuilder.Property(o => o.Country).HasMaxLength(50).IsRequired();
                           addressBuilder.Property(o => o.State).HasMaxLength(50).IsRequired();
                           addressBuilder.Property(o => o.ZibCode).HasMaxLength(5).IsRequired();
                       });
            builder.ComplexProperty(o => o.BillingAddress, addressBuilder =>
                  {
                      addressBuilder.Property(o => o.FirstName).HasMaxLength(50).IsRequired();
                      addressBuilder.Property(o => o.LastName).HasMaxLength(50).IsRequired();
                      addressBuilder.Property(o => o.EmailAddress).HasMaxLength(50).IsRequired();
                      addressBuilder.Property(o => o.AddressLine).HasMaxLength(180).IsRequired();
                      addressBuilder.Property(o => o.Country).HasMaxLength(50).IsRequired();
                      addressBuilder.Property(o => o.State).HasMaxLength(50).IsRequired();
                      addressBuilder.Property(o => o.ZibCode).HasMaxLength(5).IsRequired();
                  });
            builder.ComplexProperty(o => o.Payment, paymentBuilder =>
                              {
                                  paymentBuilder.Property(o => o.CardName).HasMaxLength(50).IsRequired();
                                  paymentBuilder.Property(o => o.CardNumber).HasMaxLength(24).IsRequired();
                                  paymentBuilder.Property(o => o.Expiration).HasMaxLength(10).IsRequired();
                                  paymentBuilder.Property(o => o.CVV).HasMaxLength(0).IsRequired();
                                  paymentBuilder.Property(o => o.PaymentMethod).HasMaxLength(50).IsRequired();

                              });

            builder.Property(o => o.Status).HasDefaultValue(OrderStatus.Draft).HasConversion(stauts => stauts.ToString(), dbstatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbstatus));
            builder.Property(o => o.TotalPrice);


        }
    }
}