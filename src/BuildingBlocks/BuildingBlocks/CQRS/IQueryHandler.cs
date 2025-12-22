using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TQuery, TRespons> : IRequestHandler<TQuery, TRespons> where TQuery : IQuery<TRespons> where TRespons : notnull
    {

    }
}