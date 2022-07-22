using System;
using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IBillTypeRepository, BillTypeRepository>();
            
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddDbContext<DataContext>(opt =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                string connStr;

                if (env == "Development")
                {
                    connStr = configuration.GetConnectionString("DefaultConnection");
                }
                else
                {
                    // var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                    // connUrl = connUrl.Replace("postgres://", string.Empty);
                    // var pgUserPass = connUrl.Split("@")[0];
                    // var pgHostPortDb = connUrl.Split("@")[1];
                    // var pgHostPort = pgHostPortDb.Split("/")[0];
                    // var pgDb = pgHostPortDb.Split("/")[1];
                    // var pgUser = pgUserPass.Split(":")[0];
                    // var pgPass = pgUserPass.Split(":")[1];
                    // var pgHost = pgHostPort.Split(":")[0];
                    // var pgPort = pgHostPort.Split(":")[1];

                    var pgHost = Environment.GetEnvironmentVariable("PG_HOST");
                    var pgPort = Environment.GetEnvironmentVariable("PG_PORT");
                    var pgUser = Environment.GetEnvironmentVariable("PG_USER");
                    var pgPassword = Environment.GetEnvironmentVariable("PG_PASSWORD");
                    var pgDb = Environment.GetEnvironmentVariable("PG_DB");

                    connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPassword};Database={pgDb};SSL Mode=Require;TrustServerCertificate=True";
                }

                opt.UseNpgsql(connStr);
                // opt.UseSqlite(_config["DefaultConnection"]);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });                        

            return services;
        }
    }
}