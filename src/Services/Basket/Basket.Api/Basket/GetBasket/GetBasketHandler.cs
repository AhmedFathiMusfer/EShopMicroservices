

using System;
using Basket.Api.Exceptions;
using Basket.Api.Models;
using BuildingBlocks.CQRS;
using FluentValidation;
using Marten;
namespace Basket.Api.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCard cart);

    public class GetBasketValdiator : AbstractValidator<GetBasketQuery>
    {
        GetBasketValdiator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("the user name is requierd");
        }
    }
    public class GetBasketHandler(IDocumentSession session) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var Basket = await session.Query<ShoppingCard>().Where(x => x.UserName == query.UserName).FirstOrDefaultAsync();
            if (Basket is null)
            {
                throw new BasketNotFound(query.UserName);
            }

            return new GetBasketResult(Basket);
        }
    }
}