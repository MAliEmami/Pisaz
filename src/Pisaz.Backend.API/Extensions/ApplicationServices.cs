using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs.Clients;
using Pisaz.Backend.API.DTOs.Clients.SignIn;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Dashboard;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Discount;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
using Pisaz.Backend.API.Models.Discount;
using Pisaz.Backend.API.Repositories;
using Pisaz.Backend.API.Services.ClientServices;

namespace Pisaz.Backend.API.Extensions
{
    public static class ApplicationServices
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepository<Client>, ClientRepository>();
            services.AddScoped<IRepository<Address>, AddressRepository>();

            services.AddScoped<IGeneralService<Client, ClientDTO, ClientAddDTO, ClientUpdateDTO>, ClientService>();
            services.AddScoped<IGeneralService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO>, AddressService>();
            services.AddScoped<IListService<DiscountCode, DiscountCodeDTO>, DiscountService>();

            services.AddDbContext<PisazDB>(oprions =>
            {
                oprions.UseSqlServer(configuration.GetConnectionString("Pisaz"));
            });
        }
    }
}