using System.Text;

namespace ThrowingStonesGame.API.Middlewares;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    private static string[] _credentials;

    public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;

        var keyValue = configuration.GetSection("Configuration").GetSection("DefaultKey").Value;
        _credentials = Encoding.UTF8.GetString(Convert.FromBase64String(keyValue)).Split(':');
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if(context.Request.Path == "/campeonato/Index")
        {
            await _next.Invoke(context);
            return;
        }
          
        var isAuthenticated = Authenticate(context);

        if (!isAuthenticated)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { mensagem = "Credenciais inválidas" });
            return;
        }

        await _next.Invoke(context);
    }

    public bool Authenticate(HttpContext context)
    {
        bool isAuthenticated = false;

        if (context.Request.Headers.Authorization.Any())
        {
            var base64decoded = Encoding.UTF8.GetString(Convert.FromBase64String(context.Request.Headers.Authorization[0].Replace("Basic ", "")));
            var basicAuth = base64decoded.Split(':');
            var username = basicAuth[0];
            var password = basicAuth[1];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return isAuthenticated;

            if (username != _credentials[0] || password != _credentials[1])
                return isAuthenticated;

            isAuthenticated = true;
        }

        return isAuthenticated;
    }
}
