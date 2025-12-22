using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface ICommndHandler<in TCommand> : IRequestHandler<TCommand, Unit> where TCommand : ICommand<Unit>
    {

    }
    public interface ICommndHandler<in TCommand, TRespons> : IRequestHandler<TCommand, TRespons> where TCommand : ICommand<TRespons> where TRespons : notnull
    {

    }
}