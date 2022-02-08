using MyLab.Log;
using MyLab.ShellTask;
using MyLab.TaskApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTaskLogic<ShellTaskLogic>();

builder.Logging.ClearProviders();
builder.Logging.AddMyLabConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseTaskApi();

app.Run();