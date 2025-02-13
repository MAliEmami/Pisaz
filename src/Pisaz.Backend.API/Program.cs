using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs.Clients;
using Pisaz.Backend.API.DTOs.Clients.SignIn;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
using Pisaz.Backend.API.Repositories;
using Pisaz.Backend.API.Services.ClientServices;
using Pisaz.Backend.API.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//
builder.Services.AddControllers();

// DataBase
builder.Services.AddDbContext<PisazDB>(oprions =>
{
    oprions.UseSqlServer(builder.Configuration.GetConnectionString("Pisaz"));
});

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

// Repository
builder.Services.AddScoped<IRepository<Client>, ClientRepository>();

// Servise
builder.Services.AddScoped<IService<Client, ClientDTO, ClientSignInDTO, ClientUpdateDTO>, ClientService>();

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();