using Notino.Converter.Helpers;
using Notino.Converter.Models;
using Notino.Homework.Api;
using Notino.Homework.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISimpleMailManager, SimpleMaiManager>();
builder.Services.AddScoped(typeof(ISerializerFactory<>), typeof(SerializerFactory<>));
builder.Services.AddScoped(typeof(IDeserializerFactory<>), typeof(DeserializerFactory<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

