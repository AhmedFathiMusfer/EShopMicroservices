using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
namespace BuildingBlocks.Behavior;

public class ValidationBehavior<TRquset, TResponse>(IEnumerable<IValidator<TRquset>> validators) : IPipelineBehavior<TRquset, TResponse>
 where TRquset : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRquset request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var context = new ValidationContext<TRquset>(request);
        var validateResult = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validateResult.Where(r => r.Errors.Any()).SelectMany(r => r.Errors).ToList();
        if (failures.Any())
        {
            throw new ValidationException(failures);
        }
        return await next();
    }
}