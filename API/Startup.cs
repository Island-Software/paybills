using System.Text;
using API.Extensions;
using API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
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
            // services.AddCors(options =>
            // {
            //     options.AddPolicy("CorsPolicy",
            //         builder => builder.AllowAnyOrigin()
            //                         .AllowAnyMethod()
            //                         .AllowAnyHeader());
            // });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

            app.UseMiddleware<ExceptionMiddleware>();

            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();                
            // }

            // Commented because it brings the CORS error even with CORS policy set
            // app.UseHttpsRedirection();                    

            app.UseRouting();

            // app.UseCors("CorsPolicy");
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

            // app.UseCors(x => x
            //     .AllowAnyMethod()
            //     .AllowAnyHeader()
            //     .SetIsOriginAllowed(origin => true)
            //     .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();

            // app.Use(next =>
            // {
            //     return async context =>
            //     {
            //         if (context.Request.Path.StartsWithSegments("/test"))
            //         {
            //             await context.Response.WriteAsync("Hit!");
            //         }
            //         else
            //         {
            //             await next(context);
            //         }
            //     };
            // });

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
