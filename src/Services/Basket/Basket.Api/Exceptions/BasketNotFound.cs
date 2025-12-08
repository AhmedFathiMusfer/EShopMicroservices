

namespace Basket.Api.Exceptions
{
    public class BasketNotFound : NotFoundException
    {
        public BasketNotFound(string UserName) : base("Basket", UserName)
        {

        }
    }
}