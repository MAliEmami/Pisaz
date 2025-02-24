using Pisaz.Backend.API.Extensions;
using Pisaz.Backend.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMySagger();

builder.Services.AddMyControllers();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddMyAuthentication(builder.Configuration);
builder.Services.AddScoped<JwtTokenService>();

var app = builder.Build();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwaggerIfDevelopment();

app.Run();