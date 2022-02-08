using MyLab.BashTask;
using MyLab.Log;
using MyLab.StatusProvider;
using MyLab.TaskApp;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddTaskLogic<ShellTaskLogic>()
    .AddAppStatusProviding();

builder.Logging
    .ClearProviders()
    .AddMyLabConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.

app
    .UseTaskApi()
    .UseStatusApi(serializerSettings: new JsonSerializerSettings
    {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore,
        TypeNameHandling = TypeNameHandling.All
    });

app.Run();