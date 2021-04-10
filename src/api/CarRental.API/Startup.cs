using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.API.Extensions;
using CarRental.Business.Abstract;
using CarRental.Business.Concrete;
using CarRental.Core.DependencyResolvers;
using CarRental.Core.Extensions;
using CarRental.Core.Utilities.IoC;
using CarRental.Core.Utilities.Security.Encryption;
using CarRental.Core.Utilities.Security.JWT;
using CarRental.DataAccess.Abstract;
using CarRental.DataAccess.Concrete.EntityFramework;
using CarRental.DataAccess.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CarRental.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. this IServiceCollection extended by ServiceExtensions.cs
        public void ConfigureServices(IServiceCollection services)
        {
            services.ServicesBaseConfiguration();
            services.AddCorsConfiguration();
            services.AddAutoMapperConfiguration();
            services.AddJwtConfiguration(Configuration.GetSection("TokenOptions").Get<TokenOptions>());
            services.AddSwaggerConfiguration();
            services.AddDbContextConfiguration(Configuration.GetConnectionString("CarRantalDb"));
            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule()
            });
        }

        // This method gets called by the runtime.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRental.API v1"));
            }
            app.UseStaticFiles();

            // Exception middleware cannot run in Development Environment
            if (!env.IsDevelopment())
            {
                //app.ConfigureCustomExceptionMiddleware();
            }

            app.UseCors("DevCors");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
