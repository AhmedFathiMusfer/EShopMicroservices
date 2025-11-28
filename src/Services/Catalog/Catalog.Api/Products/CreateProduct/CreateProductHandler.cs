
using BuildingBlocks.CQRS;
using Catalog.Api.Model;
using FluentValidation;
using Marten;
namespace Catalog.Api.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, List<string> Category, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Product description is required.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Product category is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price must be greater than zero.");
        }
    }
    public class CreateProductHandler(IDocumentSession session) : ICommndHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                price = command.Price
            };
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(Guid.NewGuid());
            //  throw new NotImplementedException();
        }
    }
}