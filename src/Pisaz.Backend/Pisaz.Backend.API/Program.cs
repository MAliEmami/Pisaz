using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DataBase
builder.Services.AddDbContext<PisazDB>(oprions =>
{
    oprions.UseSqlServer(builder.Configuration.GetConnectionString("Pisaz"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();