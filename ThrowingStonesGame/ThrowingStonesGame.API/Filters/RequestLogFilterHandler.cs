using Microsoft.AspNetCore.Mvc.Filters;
using ThrowingStonesGame.Infrastructure.EventBus;
using ThrowingStonesGame.Infrastructure.EventBus.Interfaces;

namespace ThrowingStonesGame.API.Filters
{
    public class RequestLogFilterHandler : ActionFilterAttribute
    {
        private readonly IServiceBusProducer _serviceBusProducer;
        private readonly ILogger<RequestLogFilterHandler> _logger;
        private static EventRoute? eventRoute;

        public RequestLogFilterHandler(IServiceBusProducer serviceBusProducer, ILogger<RequestLogFilterHandler> logger)
        {
            _serviceBusProducer = serviceBusProducer;
            _logger = logger;

            eventRoute ??= new()
            {
                Exchange = "throwingStonesGame.event",
                QueueName = "throwingStonesGame.api.logs",
                RoutingKey = "throwingStonesGame.api.logs"
            };
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            try
            {
                var logPayload = new
                {
                    httpVerb = filterContext.HttpContext.Request.Method,
                    statusCode = filterContext.HttpContext.Response.StatusCode,
                    dateTime = DateTime.UtcNow,
                };

                _serviceBusProducer.Publish(logPayload, eventRoute);
            }
            catch (Exception error)
            {
                _logger.LogError(error.Message, error.StackTrace);
            }
        }
    }
}
