using CallWach.API.Controller;
using CallWatch.Application;
using CallWatch.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// Serviços
builder.Services.AddTransient<CallWathController>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
// Logging
// builder.Logging.AddConsole();

// Cria o app
var app = builder.Build();

// Executa
var runner = app.Services.GetRequiredService<CallWathController>();
await runner.Execute();
