using ThrowingStonesGame.API.Filters;
using ThrowingStonesGame.Infrastructure.EventBus.Interfaces;
using ThrowingStonesGame.Infrastructure.EventBus.Service;
using ThrowingStonesGame.Infrastructure.EventBus;
using Microsoft.AspNetCore.Builder;
using ThrowingStonesGame.API.Mapping.Interfaces;
using ThrowingStonesGame.API.Mapping;

namespace ThrowingStonesGame.API.IoC.Extensions
{
    public static class DependencyGroupExtensions
    {
        public static IServiceCollection AddDependencyGroup(this IServiceCollection services)
        {
            services.AddSingleton<IServiceBusProducer, RabbitMQProducer>();
            services.AddSingleton<RequestLogFilterHandler>();
            services.AddSingleton<IMapper, Mapper>();

            return services;
        }

        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<EventBusConfiguration>(config.GetSection(nameof(EventBusConfiguration)));
            return services;
        }
    }
}
