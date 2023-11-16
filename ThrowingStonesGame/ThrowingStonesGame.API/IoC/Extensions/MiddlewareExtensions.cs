using ThrowingStonesGame.API.Middlewares;

namespace ThrowingStonesGame.API.IoC.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder AddMiddlewares(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionHandlerMiddleware>();
            applicationBuilder.UseMiddleware<AuthenticationMiddleware>();

            return applicationBuilder;
        }
    }
}