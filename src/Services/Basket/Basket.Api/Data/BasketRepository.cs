

using System.Security.AccessControl;
using Basket.Api.Exceptions;
using Basket.Api.Models;
using Marten;

namespace Basket.Api.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {

        public async Task<ShoppingCard> GetBasket(string UserName, CancellationToken cancellationToken)
        {
            var Basket = await session.LoadAsync<ShoppingCard>(UserName, cancellationToken);
            return Basket is null ? throw new BasketNotFound(UserName) : Basket;
        }

        public async Task<ShoppingCard> StoreBasket(ShoppingCard Basket, CancellationToken cancellationToken)
        {
            session.Store(Basket);
            await session.SaveChangesAsync(cancellationToken);
            return Basket;
        }

        public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken)
        {
            session.Delete(UserName);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}