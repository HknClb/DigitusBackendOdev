using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UserLoginFeature.Application.Pipelines;
using UserLoginFeature.Application.Pipelines.Behaviors;

namespace UserLoginFeature.Application
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);
            services.AddMediatR(assembly);
            services.AddValidatorsFromAssembly(assembly);

            return services;
        }

        public static IServiceCollection AddRequestValidationBehaviors(this IServiceCollection services)
        {
            PrepareRequestValidationBehaviors(services, new());
            return services;
        }

        public static IServiceCollection AddRequestValidationBehaviors(this IServiceCollection services, Action<ValidationBehaviorOptions> options)
        {
            ValidationBehaviorOptions opt = new();
            options(opt);

            PrepareRequestValidationBehaviors(services, opt);

            return services;
        }

        private static void PrepareRequestValidationBehaviors(IServiceCollection services, ValidationBehaviorOptions options)
        {
            if (options.AddValidationErrorsToList)
            {
                if (options.ThrowValidationErrors)
                    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestThrowValidationBehavior<,>));
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestAddListValidationBehavior<,>));
            }
            else
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        }
    }
}
