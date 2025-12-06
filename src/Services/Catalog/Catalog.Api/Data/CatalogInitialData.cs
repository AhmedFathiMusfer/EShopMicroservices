

using Catalog.Api.Model;
using Marten;
using Marten.Schema;

namespace Catalog.Api.Data
{
    public class CatalogInitialData : IInitialData

    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {

            using var sesstion = store.LightweightSession();
            if (await sesstion.Query<Model.Product>().AnyAsync())
                return;

            sesstion.Store<Product>(getPreConfiguredProducts());
            await sesstion.SaveChangesAsync();
        }

        private static IEnumerable<Product> getPreConfiguredProducts()
        {
            return new List<Product>
{
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Wireless Mouse",
        Category = new List<string> { "Electronics", "Accessories" },
        Description = "Ergonomic wireless mouse with adjustable DPI and long battery life.",
        ImageFile = "mouse.jpg",
        price = 29.99m
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Gaming Keyboard",
        Category = new List<string> { "Electronics", "Gaming" },
        Description = "Mechanical keyboard with RGB lighting and programmable keys.",
        ImageFile = "keyboard.jpg",
        price = 79.99m
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Smartphone",
        Category = new List<string> { "Electronics", "Mobile" },
        Description = "Latest model smartphone with high-resolution camera and fast processor.",
        ImageFile = "smartphone.jpg",
        price = 599.99m
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Bluetooth Headphones",
        Category = new List<string> { "Electronics", "Audio" },
        Description = "Over-ear Bluetooth headphones with noise cancellation.",
        ImageFile = "headphones.jpg",
        price = 149.99m
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Office Chair",
        Category = new List<string> { "Furniture", "Office" },
        Description = "Ergonomic office chair with lumbar support and adjustable height.",
        ImageFile = "office_chair.jpg",
        price = 199.99m
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Running Shoes",
        Category = new List<string> { "Sports", "Footwear" },
        Description = "Lightweight running shoes with breathable material and good grip.",
        ImageFile = "running_shoes.jpg",
        price = 89.99m
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Coffee Maker",
        Category = new List<string> { "Home Appliances", "Kitchen" },
        Description = "Automatic coffee maker with programmable timer and strong brew option.",
        ImageFile = "coffee_maker.jpg",
        price = 129.99m
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "LED Desk Lamp",
        Category = new List<string> { "Home Appliances", "Lighting" },
        Description = "Energy-saving LED desk lamp with adjustable brightness and color temperature.",
        ImageFile = "desk_lamp.jpg",
        price = 39.99m
    }
};
        }
    }
}