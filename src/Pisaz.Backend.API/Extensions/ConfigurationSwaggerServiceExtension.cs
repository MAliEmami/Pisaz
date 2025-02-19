using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Extensions
{
    public static class DevelopmentService
    {
        public static void AddMySagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
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