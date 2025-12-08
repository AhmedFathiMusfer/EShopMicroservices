

using Basket.Api.Models;

namespace Basket.Api.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCard> GetBasket(string UserName, CancellationToken cancellationToken);
        Task<ShoppingCard> StoreBasket(ShoppingCard Basket, CancellationToken cancellationToken);

        Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken);
    }
}