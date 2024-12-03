using Domain.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Application;
using Microsoft.Win32;
using Infrastructure.JwtFeatures;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Application.IServices;
using System.Security.Claims;
using Infrastructure.Persistence.Context;
using Infrastructure.Email;

using FluentEmail.Smtp;
using System.Net.Mail;
namespace Infrastructure
{
    public static class DependecyInjection
    {

        //This method is used to register infrastructure-related services and dependencies into the DI container
        //By using an extension method, you can keep your Program.cs file clean and organized
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            #region

            services.AddDbContext<ApplicationDbContext>(options => 
            {
                var connectionString = configuration.GetConnectionString("onlineShopDb");
                options.UseNpgsql(connectionString);

            });
            #endregion

            #region Identity Configuration
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            #endregion





            #region JWT Authentication Configuration
            var jwtSettings = configuration.GetSection("JwtSettings");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
                    //RoleClaimType = ClaimTypes.Role // Ensures roles are recognized
                };
                // Thêm event handlers để debug
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated successfully");
                        return Task.CompletedTask;
                    }
                };
            });
            #endregion

            services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyAdminUsers",
                    policy => policy.RequireRole("Admin"));
            });


            //var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            //services.AddSingleton(emailConfig);

            // Load SMTP settings from configuration
            var smtpSettings = configuration.GetSection("SmtpSettings").Get<SmtpSettings>();

            services.AddFluentEmail(smtpSettings.FromEmail, smtpSettings.FromName)
            .AddRazorRenderer()
            .AddSmtpSender(new SmtpClient(smtpSettings.Host)
            {
                Port = smtpSettings.Port,
                Credentials = new System.Net.NetworkCredential(smtpSettings.UserName,
            smtpSettings.Password),
                EnableSsl = true,
            });


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #region Inject Repository
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            services.AddScoped< IJwtService, JwtHandler>();
            services.AddScoped<IEmailSender, EmailSender>();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll", builder =>
            //    {
            //        builder.AllowAnyOrigin()
            //               .AllowAnyMethod()
            //               .AllowAnyHeader();
            //    });
            //});



            return services;
        }
    }
}
