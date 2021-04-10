using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Core.Utilities.Security.Encryption;
using CarRental.Core.Utilities.Security.JWT;
using CarRental.DataAccess.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace CarRental.API.Extensions
{
    public static class ServiceExtensions
    {

        public static IServiceCollection ServicesBaseConfiguration(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(builder => {
                builder.AddPolicy("DevCors",
                    opt => opt.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()
                );
            });
            return services;
        }

        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            return services;
        }

        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, TokenOptions tokenOptions )
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters=new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

            return services;
        }

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Kiralamax.API v1", new OpenApiInfo { Title = "Kiralamax.API", Version = "v1" });
            });
            return services;
        }

        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CarRentalDbContext>(options =>
                options.UseSqlServer(connectionString),ServiceLifetime.Transient);
            return services;
        }

    }
}
