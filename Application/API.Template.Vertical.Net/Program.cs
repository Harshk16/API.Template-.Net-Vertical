using API.Template.Vertical.Net.Common.Extensions;
using API.Template.Vertical.Net.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseApplicationPipeline();

app.Run();
