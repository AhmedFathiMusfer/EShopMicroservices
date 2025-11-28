

using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Model;
using FluentValidation;
using Marten;
namespace Catalog.Api.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid id, string Name, string Description, List<string> Category, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool isSuccess);
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator(

          )
        {
            RuleFor(command => command.id).NotEmpty().WithName("Product ID is required");
            RuleFor(command => command.Name).NotEmpty().WithName("Product Name is required").Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price must be greater than zero.");
        }
    }
    public class UpdateProductHandler(IDocumentSession session) : ICommndHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException();
            }
            product.Category = command.Category;
            product.Name = command.Name;
            product.Description = command.Description;
            product.price = command.Price;
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
            //  throw new NotImplementedException();
        }
    }
}