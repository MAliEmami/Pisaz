using FluentValidation.AspNetCore;
using Pisaz.Backend.API.Extensions;
using Pisaz.Backend.API.Validations.ClientValidations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMySagger();

builder.Services.AddMyControllers();

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
app.MapControllers();

app.UseSwaggerIfDevelopment();

app.Run();