
using BuildingBlocks.CQRS;
using Catalog.Api.Model;
using Marten;

namespace Catalog.Api.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);


    internal class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryHandler.Handel called with {@Query} ", query);
            var products = await session.Query<Product>().Where(p => p.Category.Contains(query.category)).ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(products);

        }
    }
}