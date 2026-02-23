


using System.Net.Http.Headers;
using Basket.Api.Data;
using Basket.Api.Dtos;
using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using FluentValidation;
using Mapster;
using MassTransit;

namespace Basket.Api.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(CheckoutBasketDto CheckoutBasketDto) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsScuccess);

    public class CheckoutBasketValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketValidator()
        {
            RuleFor(c => c.CheckoutBasketDto).NotNull().WithMessage("CheckoutBasketDto can not by null");
            RuleFor(c => c.CheckoutBasketDto.UserName).NotEmpty().WithMessage("UserName is required");

        }
    }
    public class heckoutBasketHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint) : ICommndHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(command.CheckoutBasketDto.UserName, cancellationToken);
            if (basket == null)
            {
                return new CheckoutBasketResult(false);
            }
            var eventMessage = command.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;
            await publishEndpoint.Publish(eventMessage);
            await basketRepository.DeleteBasket(command.CheckoutBasketDto.UserName, cancellationToken);
            return new CheckoutBasketResult(true);
        }
    }
}