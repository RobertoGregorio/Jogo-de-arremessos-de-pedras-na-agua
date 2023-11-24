namespace ThrowingStonesGame.API.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            var erroResponse = new
            {
                mensagem = ex.Message,
                tipoDeErro = ex.GetType().ToString(),
            };

            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(erroResponse);
        }
    }
}