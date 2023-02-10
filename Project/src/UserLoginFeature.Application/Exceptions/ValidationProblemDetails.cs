using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UserLoginFeature.Application.Exceptions;

public class ValidationProblemDetails : ProblemDetails
{
    public object Errors { get; set; } = null!;

    public override string ToString() => JsonConvert.SerializeObject(this);
}