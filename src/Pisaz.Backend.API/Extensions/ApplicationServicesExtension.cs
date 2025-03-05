using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs.Clients;
using Pisaz.Backend.API.DTOs.Clients.SignIn;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Cart;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Dashboard;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Discount;
using Pisaz.Backend.API.DTOs.ClientsDTOs.ReferalCode;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
using Pisaz.Backend.API.Models.Discount;
using Pisaz.Backend.API.Models.Product.Cart;
using Pisaz.Backend.API.Repositories;
using Pisaz.Backend.API.Services;
using Pisaz.Backend.API.Services.ClientServices;
//using Pisaz.Backend.API.Validations.ClientValidations;

namespace Pisaz.Backend.API.Extensions
{
    public static class ApplicationServices
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IRepository<Client>, ClientRepository>();
            services.AddScoped<ClientRepository>();
            services.AddScoped<IRepository<Address>, AddressRepository>();
            services.AddScoped<LoginRequestRepository>();
            services.AddScoped<ShoppingCartRepository>();
            services.AddScoped<DiscountRepository>();

            services.AddScoped<IGeneralService<Client, ClientDTO, ClientAddDTO, ClientUpdateDTO>, ClientService>();
            //services.AddScoped<IGeneralService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO>, AddressService>();
            
            services.AddScoped<IQueryService<Address, AddressDTO>, AddressService>();
            services.AddScoped<ICommandService<Address, AddressAddDTO, AddressUpdateDTO>, AddressService>();

            services.AddScoped<IQueryService<DiscountCode, DiscountCodeDTO>, DiscountService>();
            services.AddScoped<IQueryService<ShoppingCart, CartStatusDTO>, CartStatusService>();
            services.AddScoped<IQueryService<Refers, RefersDTO>, RefersSystemService>();
            services.AddScoped<IQueryService<DiscountCode ,DiscountCodeDTO>, DiscountService>();
            services.AddScoped<IQueryService<Products ,PurchaseHistoryDTO>, PurchaseHistoryService>();
            
            services.AddScoped<AuthService>();


            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });

            services.AddDbContext<PisazDB>(oprions =>
            {
                oprions.UseSqlServer(configuration.GetConnectionString("Pisaz"));
            });
        }
        public static IServiceCollection AddMyControllers(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            return services;
        }

        public static IServiceCollection AddMyAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Authentication failed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated successfully.");
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization();

            return services;
        }
    }
}