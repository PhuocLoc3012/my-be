using Application.IServices;
using Application.Mappers;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();


            return services;
        }
    }
}
