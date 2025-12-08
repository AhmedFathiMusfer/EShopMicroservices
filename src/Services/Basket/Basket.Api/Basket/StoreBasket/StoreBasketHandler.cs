
using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Basket.Api.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCard Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);
    public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("the user name is required");


        }
    }

    public class StoreBasketHandler(IBasketRepository repository) : ICommndHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var cart = await repository.StoreBasket(command.Cart, cancellationToken);
            return new StoreBasketResult(cart.UserName);
        }
    }
}