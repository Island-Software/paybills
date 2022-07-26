using API.Extensions;
using API.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Certificate;

namespace API
{
    public class Startup
    {
        public IConfiguration _config { get; }
        public Startup(IConfiguration config)
        {
            this._config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(
                CertificateAuthenticationDefaults.AuthenticationScheme)
                .AddCertificate();
            services.AddApplicationServices(_config);
            services.AddControllers();
            services.AddIdentityServices(_config);
            services.AddCors();            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

            app.UseMiddleware<ExceptionMiddleware>();

            // if (env.IsProduction())
                app.UseHttpsRedirection();                    

            app.UseRouting();

            app.UseCors(x => x.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("https://localhost:4200"));


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            // if (env.IsProduction())
            // {
                app.UseDefaultFiles();
                app.UseStaticFiles();
            // }            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // if (env.IsProduction())
                    endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
