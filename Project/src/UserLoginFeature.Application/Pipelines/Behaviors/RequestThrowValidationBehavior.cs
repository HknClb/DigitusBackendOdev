using FluentValidation;
using FluentValidation.Results;
using MediatR;
using UserLoginFeature.Application.Requests;

namespace UserLoginFeature.Application.Pipelines.Behaviors;

public class RequestThrowValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, IThrowValidationErrors
{
    // All validators that about CQRS request
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestThrowValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if ((request as IAddValidationErrorsToList<ValidationFailure>)?.ValidationFailures is not null)
            return next();
        ValidationContext<object> context = new(request);
        List<ValidationFailure> failures = _validators
                                           .Select(validator => validator.Validate(context))
                                           .SelectMany(result => result.Errors)
                                           .Where(failure => failure != null)
                                           .ToList();

        if (failures.Count != 0) throw new ValidationException(failures);
        return next();
    }
}