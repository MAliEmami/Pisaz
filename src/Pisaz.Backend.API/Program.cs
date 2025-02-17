using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs.Clients;
using Pisaz.Backend.API.DTOs.Clients.SignIn;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Dashboard;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
using Pisaz.Backend.API.Repositories;
using Pisaz.Backend.API.Services.ClientServices;
//using Pisaz.Backend.API.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// DataBase
builder.Services.AddDbContext<PisazDB>(oprions =>
{
    oprions.UseSqlServer(builder.Configuration.GetConnectionString("Pisaz"));
});

// Repository
builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
builder.Services.AddScoped<IRepository<Address>, AddressRepository>();

// Servise
builder.Services.AddScoped<IGeneralService<Client, ClientDTO, ClientAddDTO, ClientUpdateDTO>, ClientService>();
builder.Services.AddScoped<IGeneralService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO>, AddressService>();

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();