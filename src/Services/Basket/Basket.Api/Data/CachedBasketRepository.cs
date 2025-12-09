
using System.Text.Json;
using Basket.Api.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Api.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {


        public async Task<ShoppingCard> GetBasket(string UserName, CancellationToken cancellationToken)
        {
            var cacheBasket = await cache.GetStringAsync(UserName, cancellationToken);
            if (!string.IsNullOrEmpty(cacheBasket))
                return JsonSerializer.Deserialize<ShoppingCard>(cacheBasket)!;

            var basket = await repository.GetBasket(UserName, cancellationToken);
            await cache.SetStringAsync(UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCard> StoreBasket(ShoppingCard Basket, CancellationToken cancellationToken)
        {
            await repository.StoreBasket(Basket, cancellationToken);
            await cache.SetStringAsync(Basket.UserName, JsonSerializer.Serialize(Basket), cancellationToken);
            return Basket;

        }
        public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken)
        {
            await repository.DeleteBasket(UserName, cancellationToken);
            await cache.RemoveAsync(UserName, cancellationToken);
            return true;
        }
    }
}