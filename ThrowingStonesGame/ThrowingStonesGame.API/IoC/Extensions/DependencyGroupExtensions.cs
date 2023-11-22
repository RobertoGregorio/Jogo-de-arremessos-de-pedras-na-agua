using ThrowingStonesGame.API.Filters;
using ThrowingStonesGame.API.Mapping;
using ThrowingStonesGame.Application.Interfaces.Mapping;
using ThrowingStonesGame.Application.Interfaces.Service;
using ThrowingStonesGame.Application.Services;
using ThrowingStonesGame.Infrastructure.EventBus;
using ThrowingStonesGame.Infrastructure.EventBus.Interfaces;
using ThrowingStonesGame.Infrastructure.EventBus.Service;

namespace ThrowingStonesGame.API.IoC.Extensions
{
    public static class DependencyGroupExtensions
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
    }
}
