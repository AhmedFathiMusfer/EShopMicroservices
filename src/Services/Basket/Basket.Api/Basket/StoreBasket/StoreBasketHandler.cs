
using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.CQRS;
using Discount.Grpc;
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

    public class StoreBasketHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommndHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await dedcutDiscount(command.Cart, cancellationToken);
            var cart = await repository.StoreBasket(command.Cart, cancellationToken);
            return new StoreBasketResult(cart.UserName);
        }
        private async Task dedcutDiscount(ShoppingCard cart, CancellationToken cancellationToken)
        {
            // communication with discount grpc and calculate  the discount for all products in the cart
            foreach (var item in cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
    }
}