


using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;
    public record DeleteOrderResult(bool IsSuccess);

    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(c => c.OrderId).NotEmpty().WithMessage("Id is required");

        }
    }

}