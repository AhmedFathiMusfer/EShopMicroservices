using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQuery<out TRespons> : IRequest<TRespons> where TRespons : notnull
    {

    }
}