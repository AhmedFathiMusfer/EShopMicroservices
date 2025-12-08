using Basket.Api.Data;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Basket.Api.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool ISsuccess);
    public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("the user name is required");
        }
    }
    public class DeleteBasketHandler(IBasketRepository repository) : ICommndHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteBasket(command.UserName, cancellationToken);
            return new DeleteBasketResult(result);
        }
    }

}