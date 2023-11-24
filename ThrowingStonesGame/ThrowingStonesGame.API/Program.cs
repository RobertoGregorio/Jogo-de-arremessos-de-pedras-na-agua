using ThrowingStonesGame.API.IoC.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddModelBidingCustomValidation();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddConfig(builder.Configuration);

builder.Services.AddDependencyGroup();

var app = builder.Build();

app.MapControllerRoute(
    name: "index",
    pattern: "{controller=ThrowingStonesGame}/{action=Index}");

app.AddMiddlewares();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
