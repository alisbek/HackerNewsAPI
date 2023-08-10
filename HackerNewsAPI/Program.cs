using HackerNewsAPI.Models;
using HackerNewsAPI.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.Configure<HackerNewsAPIConfig>(configuration.GetSection("HackerNewsAPI"));
builder.Services.AddHttpClient();
builder.Services.AddTransient<HackerNewsAPIService>(); 



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
