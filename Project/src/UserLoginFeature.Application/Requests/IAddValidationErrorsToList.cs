using FluentValidation.Results;

namespace UserLoginFeature.Application.Requests
{
    public interface IAddValidationErrorsToList<TValidationFailure> where TValidationFailure : ValidationFailure
    {
        IList<TValidationFailure>? ValidationFailures { get; set; }
    }
}
