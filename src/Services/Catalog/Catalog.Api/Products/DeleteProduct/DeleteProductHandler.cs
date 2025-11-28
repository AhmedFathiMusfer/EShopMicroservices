

using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Model;
using FluentValidation;
using Marten;
namespace Catalog.Api.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool isSuccess);
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(command => command.id).NotEmpty().WithName("Product ID is required");
        }
    }
    public class DeleteProductHandler(IDocumentSession session) : ICommndHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {

            session.Delete<Product>(command.id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);

        }
    }
}