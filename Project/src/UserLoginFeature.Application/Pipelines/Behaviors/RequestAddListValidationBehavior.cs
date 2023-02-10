using FluentValidation;
using FluentValidation.Results;
using MediatR;
using UserLoginFeature.Application.Requests;

namespace UserLoginFeature.Application.Pipelines.Behaviors;

public class RequestAddListValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, IAddValidationErrorsToList<ValidationFailure>
{
    // All validators that about CQRS request
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestAddListValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.ValidationFailures is null)
            return next();

        // Todo I should create a new ValidationResults class for escape dependencies
        ValidationContext<object> context = new(request);
        List<ValidationFailure> failures = _validators
                                           .Select(validator => validator.Validate(context))
                                           .SelectMany(result => result.Errors)
                                           .Where(failure => failure != null)
                                           .ToList();

        if (failures.Count != 0)
        {
            foreach (ValidationFailure failure in failures)
                request.ValidationFailures.Add(failure);
        }
        return next();
    }
}