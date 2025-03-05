using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace Pisaz.Backend.API.Extensions
{
    public static class DevelopmentService
    {
        public static void AddMySagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PisApp API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name          = "Authorization",
                    Type          = SecuritySchemeType.ApiKey,
                    Scheme        = "Bearer",
                    BearerFormat  = "JWT",
                    In            = ParameterLocation.Header,
                    Description   = "Please Enter Your Bearer Token"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type  = ReferenceType.SecurityScheme,
                                Id    = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }   
    }


    public static class DevelopmentUse
    {
        public static void UseSwaggerIfDevelopment(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}