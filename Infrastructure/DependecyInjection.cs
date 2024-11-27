using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            #region

            services.AddDbContext<ApplicationDbContext>(options => 
            {
                var connectionString = configuration.GetConnectionString("onlineShopDb");
                options.UseNpgsql(connectionString);

            });


            #endregion




            return services;
        }
    }
}
