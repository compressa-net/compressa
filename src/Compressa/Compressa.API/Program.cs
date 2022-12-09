using Compressa.API.Models.Logging;
using Microsoft.Extensions.Logging.Console;

var builder = WebApplication.CreateBuilder(args);

// Change the default logger class to a simpler one
builder.Logging.ClearProviders();
builder.Logging.AddConsoleFormatter<CustomSimpleConsoleFormatter, SimpleConsoleFormatterOptions>();
builder.Logging.AddConsole(options => options.FormatterName = "customSimpleConsoleFormatter");
builder.Logging.AddDebug();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
