using ThrowingStonesGame.API.Filters;
using ThrowingStonesGame.API.Middlewares;
using ThrowingStonesGame.Infrastructure.EventBus;
using ThrowingStonesGame.Infrastructure.EventBus.Interfaces;
using ThrowingStonesGame.Infrastructure.EventBus.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<EventBusConfiguration>(builder.Configuration.GetSection(nameof(EventBusConfiguration)));
builder.Services.AddSingleton<IServiceBusProducer, RabbitMQProducer>();
builder.Services.AddSingleton<RequestLogFilterHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<AuthenticationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
