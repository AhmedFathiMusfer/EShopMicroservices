

using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extentions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers => new List<Customer>
         {
    Customer.Create(
        CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
        "Ahmed",
        "ahmed@gmail.com"
    ),
    Customer.Create(
        CustomerId.Of(new Guid("d12f6b2a-7b44-4a66-9c9d-1b8c8c0b1234")),
        "Ali",
        "ali@gmail.com"
    )
};
        public static IEnumerable<Product> Products => new List<Product>
{
    Product.Create(
        ProductId.Of(new Guid("b2a7b44a-4a66-4c9d-9c9d-1b8c8c0b1201")),
        "IPhone X",
        500),

    Product.Create(
        ProductId.Of(new Guid("c3f8a123-5b77-4d11-8a22-2c9d9d1c2302")),
        "Samsung S21",
        650),

    Product.Create(
        ProductId.Of(new Guid("d4e9b234-6c88-4e22-9b33-3d1e2e2d3403")),
        "MacBook Pro",
        1500),

    Product.Create(
        ProductId.Of(new Guid("e5fab345-7d99-4f33-ac44-4e2f3f3e4504")),
        "Dell XPS 13",
        1200),

    Product.Create(
        ProductId.Of(new Guid("f6abc456-8e10-4a44-bd55-5f3a4a4f5605")),
        "IPad Air",
        700)
};

        public static IEnumerable<Order> Orders
        {
            get
            {
                var address1 = Address.Of(
                        "Ahmed",
                        "Ali",
                        "ahmed@gmail.com",
                        "Tokyo Street 10",
                        "Japan",
                        "Tokyo",
                        "10001");
                var address2 = Address.Of(
                     "Ali",
                     "Mohammed",
                     "ali@gmail.com",
                     "Osaka Street 20",
                     "Japan",
                     "Osaka",
                     "20002");


                var payment1 = Payment.Of(
                       "Ahmed Ali",
                       "1234567812345678",
                       "12/28",
                       "123",
                       1);
                var payment2 = Payment.Of(
                      "Ali Mohammed",
                      "8765432187654321",
                      "11/27",
                      "456",
                      2);
                var order1 = Order.Create(
                    OrderId.Of(new Guid("11111111-1111-1111-1111-111111111111")),
                    CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
                    OrderName.Of("ORD-1"),
                     address1,
                    address1,
                    payment1
                );

                order1.Add(
                    ProductId.Of(new Guid("b2a7b44a-4a66-4c9d-9c9d-1b8c8c0b1201")),
                    2,
                    500);

                order1.Add(
                    ProductId.Of(new Guid("c3f8a123-5b77-4d11-8a22-2c9d9d1c2302")),
                    1,
                    650);



                var order2 = Order.Create(
                    OrderId.Of(new Guid("22222222-2222-2222-2222-222222222222")),
                    CustomerId.Of(new Guid("d12f6b2a-7b44-4a66-9c9d-1b8c8c0b1234")),
                    OrderName.Of("ORD-2"),
                   address2,
                   address2,
                   payment2
                );

                order2.Add(
                    ProductId.Of(new Guid("d4e9b234-6c88-4e22-9b33-3d1e2e2d3403")),
                    1,
                    1500);

                return new List<Order>
        {
            order1,
            order2
        };
            }
        }

    }
}