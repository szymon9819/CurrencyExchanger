using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

app.MapApiEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
