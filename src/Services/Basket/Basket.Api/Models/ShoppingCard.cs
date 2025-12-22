namespace Basket.Api.Models
{
    public class ShoppingCard
    {
        public string UserName { get; set; } = default!;
        public List<ShoppingCardItem> Items { get; set; } = new();

        public decimal TotalPrice => Items.Sum(x => x.Quantity * x.Price);

        public ShoppingCard(string userName)
        {
            UserName = userName;
        }
        public ShoppingCard()
        {

        }
    }
}