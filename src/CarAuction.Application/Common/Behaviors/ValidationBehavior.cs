using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;

namespace CarAuction.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators) 
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
        where TResponse : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context)));

        var validationFailures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        var validationErrors = validationResults
            .SelectMany(r => r.AsErrors())
            .ToList();

        if (validationErrors.Count == 0) return await next();

        if (IsResultType(typeof(TResponse)))
            return (dynamic)Result.Invalid(validationErrors);
            
        throw new ValidationException(validationFailures);
    }

    #region private
    private static bool IsResultType(Type responseType)
    {
        return responseType == typeof(Result) ||
               (responseType.IsGenericType
                && responseType.GetGenericTypeDefinition() == typeof(Result<>));
    }
    #endregion
}



