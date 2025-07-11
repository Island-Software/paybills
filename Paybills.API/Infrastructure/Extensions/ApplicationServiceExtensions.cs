using Paybills.API.Data;
using Paybills.API.Helpers;
using Paybills.API.Interfaces;
using Paybills.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Paybills.API.Domain.Services;
using Paybills.API.Domain.Services.Interfaces;
using Paybills.API.Domain.Services.Impl;
using Paybills.API.Infrastructure.Data.Repositories.Impl;
using Paybills.API.Infrastructure.Data.Repositories.Interfaces;

namespace Paybills.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IBillTypeService, BillTypeService>();
            services.AddScoped<IReceivingService, ReceivingService>();
            services.AddScoped<IReceivingTypeService, ReceivingTypeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IBillTypeRepository, BillTypeRepository>();
            services.AddScoped<IReceivingRepository, ReceivingRepository>();
            services.AddScoped<IReceivingTypeRepository, ReceivingTypeRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddDbContext<DataContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

            return services;
        }
    }
}