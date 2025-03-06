using Pisaz.Backend.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMyControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMySagger();

builder.Services.AddMyAuthentication(builder.Configuration);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseSwaggerIfDevelopment();

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();