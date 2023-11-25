using Microsoft.AspNetCore.Mvc;
using ThrowingStonesGame.API.Filters;
using ThrowingStonesGame.API.Mapping;
using ThrowingStonesGame.Application.Interfaces.Mapping;
using ThrowingStonesGame.Application.Interfaces.Service;
using ThrowingStonesGame.Application.Models.Response;
using ThrowingStonesGame.Application.Services;
using ThrowingStonesGame.Infrastructure.EventBus;
using ThrowingStonesGame.Infrastructure.EventBus.Interfaces;
using ThrowingStonesGame.Infrastructure.EventBus.Service;

namespace ThrowingStonesGame.API.IoC.Extensions;

public static class PipelineBuildExtensions
{
    public static IServiceCollection AddDependencyGroup(this IServiceCollection services)
    {
        services.AddSingleton<IServiceBusProducer, RabbitMQProducer>();
        services.AddSingleton<RequestLogFilterHandler>();
        services.AddSingleton<IThrowingStonesGameMapper, ThrowingStonesGameMapper>();
        services.AddSingleton<IThrowingStonesGameRankingService, ThrowingStonesGameRankingService>();
        services.AddSingleton<IThrowingStonesGameService, ThrowingStonesGameService>();

        return services;
    }

    public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<EventBusConfiguration>(config.GetSection(nameof(EventBusConfiguration)));
        return services;
    }

    public static IServiceCollection AddModelBidingCustomValidation(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => new
                    {
                        Name = e.Key,
                        Message = e.Value.Errors.First().ErrorMessage
                    }).ToArray();

                var modelValidationResponse = new ModelValidation("Os dados não foram informados corretamente", errors);

                return new BadRequestObjectResult(modelValidationResponse);
            };
        });

        return services;
    }
}
