using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Net;
using System.Web;

namespace UserLoginFeature.Application.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            Task? task = HandleExceptionAsync(context, exception);
            if (task is not null)
                await task;
        }
    }

    private Task? HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        if (context.Request.Path.ToString().Contains("/api"))
        {
            if (exception.GetType() == typeof(ValidationException)) return CreateValidationException(context, exception);
            if (exception.GetType() == typeof(BusinessException)) return CreateBusinessException(context, exception);
            if (exception.GetType() == typeof(AuthorizationException)) return CreateAuthorizationException(context, exception);
            return CreateInternalException(context, exception);
        }
        if (exception.GetType() == typeof(ValidationException)) CreateValidationExceptionForMvc(context, exception);
        else if (exception.GetType() == typeof(BusinessException)) CreateBusinessExceptionForMvc(context, exception);
        else if (exception.GetType() == typeof(AuthorizationException)) CreateAuthorizationExceptionForMvc(context, exception);
        else CreateInternalExceptionForMvc(context, exception);
        return null;

    }

    private Task CreateAuthorizationException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);

        return context.Response.WriteAsync(new AuthorizationProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Type = "https://example.com/probs/authorization",
            Title = "Authorization exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private Task CreateBusinessException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

        return context.Response.WriteAsync(new BusinessProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/probs/business",
            Title = "Business exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private Task CreateValidationException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
        object errors = ((ValidationException)exception).Errors;

        return context.Response.WriteAsync(new ValidationProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/probs/validation",
            Title = "Validation error(s)",
            Detail = "",
            Instance = "",
            Errors = errors
        }.ToString());
    }

    private Task CreateInternalException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);

        return context.Response.WriteAsync(new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://example.com/probs/internal",
            Title = "Internal exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private void CreateBusinessExceptionForMvc(HttpContext context, Exception exception)
    {
        BusinessProblemDetails problemDetails = new BusinessProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/probs/business",
            Title = "Business exception",
            Detail = exception.Message,
            Instance = ""
        };
        NameValueCollection queryStringCollection = HttpUtility.ParseQueryString(string.Empty);
        queryStringCollection.Add("Status", problemDetails.Status.ToString());
        queryStringCollection.Add("Type", problemDetails.Type);
        queryStringCollection.Add("Title", problemDetails.Title);
        queryStringCollection.Add("Detail", problemDetails.Detail);
        queryStringCollection.Add("Instance", problemDetails.Instance);
        context.Response.Redirect("/Errors/BusinessError?" + queryStringCollection.ToString(), false);
    }

    private void CreateValidationExceptionForMvc(HttpContext context, Exception exception)
    {
        var errors = ((ValidationException)exception).Errors.ToArray();
        ValidationProblemDetails problemDetails = new ValidationProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/probs/validation",
            Title = "Validation error(s)",
            Detail = exception.Message,
            Instance = "",
            Errors = errors
        };
        NameValueCollection queryStringCollection = HttpUtility.ParseQueryString(string.Empty);
        queryStringCollection.Add("Status", problemDetails.Status?.ToString());
        queryStringCollection.Add("Type", problemDetails.Type);
        queryStringCollection.Add("Title", problemDetails.Title);
        queryStringCollection.Add("Detail", problemDetails.Detail);
        queryStringCollection.Add("Instance", problemDetails.Instance);
        for (int i = 0; i < errors.Length; i++)
        {
            var error = errors[i];
            queryStringCollection.Add($"errors[{i}].PropertyName", error.PropertyName);
            queryStringCollection.Add($"errors[{i}].ErrorMessage", error.ErrorMessage);
            queryStringCollection.Add($"errors[{i}].AttemptedValue", error.AttemptedValue?.ToString());
            queryStringCollection.Add($"errors[{i}].CustomState", error.CustomState?.ToString());
            queryStringCollection.Add($"errors[{i}].Severity", error.Severity.ToString());
            queryStringCollection.Add($"errors[{i}].ErrorCode", error.ErrorCode);
        }
        context.Response.Redirect("/Errors/ValidationError?" + queryStringCollection.ToString(), false);
    }

    private void CreateInternalExceptionForMvc(HttpContext context, Exception exception)
    {
        ProblemDetails problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://example.com/probs/internal",
            Title = "Internal exception",
            Detail = exception.Message,
            Instance = ""
        };
        NameValueCollection queryStringCollection = HttpUtility.ParseQueryString(string.Empty);
        queryStringCollection.Add("Status", problemDetails.Status.ToString());
        queryStringCollection.Add("Type", problemDetails.Type);
        queryStringCollection.Add("Title", problemDetails.Title);
        queryStringCollection.Add("Detail", problemDetails.Detail);
        queryStringCollection.Add("Instance", problemDetails.Instance);
        context.Response.Redirect("/Errors/InternalError?" + queryStringCollection.ToString(), false);
    }

    private void CreateAuthorizationExceptionForMvc(HttpContext context, Exception exception)
    {
        AuthorizationProblemDetails problemDetails = new AuthorizationProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Type = "https://example.com/probs/authorization",
            Title = "Authorization exception",
            Detail = exception.Message,
            Instance = ""
        };
        NameValueCollection queryStringCollection = HttpUtility.ParseQueryString(string.Empty);
        queryStringCollection.Add("Status", problemDetails.Status.ToString());
        queryStringCollection.Add("Type", problemDetails.Type);
        queryStringCollection.Add("Title", problemDetails.Title);
        queryStringCollection.Add("Detail", problemDetails.Detail);
        queryStringCollection.Add("Instance", problemDetails.Instance);
        context.Response.Redirect("/Errors/AuthorizationError?" + queryStringCollection.ToString(), false);
    }
}