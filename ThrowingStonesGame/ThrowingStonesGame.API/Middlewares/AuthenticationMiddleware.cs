using System.Text;

namespace ThrowingStonesGame.API.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly string[] Credentials = { "seleção2022", "rubyonrails" };

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

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

                if (username != Credentials[0] || password != Credentials[1])
                    return isAuthenticated;

                isAuthenticated = true;
            }


            return isAuthenticated;


        }
    }
}
