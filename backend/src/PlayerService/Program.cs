using BIS.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddBaseConfiguration();
builder.AddBISLogging();

builder.Services.AddBISApiServices();
builder.Services.AddPlayerServices(builder.Configuration);

var app = builder.Build();

app.UseBISApiPipeline();

app.RunWithLogging("PlayerService");
