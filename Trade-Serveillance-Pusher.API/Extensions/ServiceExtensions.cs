using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Trade_Serveillance_Pusher.CORE.Context;
using Trade_Serveillance_Pusher.CORE.Repository;
using Trade_Serveillance_Pusher.CORE.Repository.TableDependency;

namespace Trade_Serveillance_Pusher.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Trade Serveillance Pusher.API", Version = "v1" });
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });

        }
        // Cors
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }
        // Database Configuration
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:DefaultConnection"];
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<TradeServeillanceDbContext>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationsAssembly));
                options.EnableSensitiveDataLogging(true);

            });

            var ecxConnectionString = config["ConnectionStrings:ECXConnection"];
            var ecxMigrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<EcxDbContext>(options =>
            {
                options.UseSqlServer(ecxConnectionString, b => b.MigrationsAssembly(ecxMigrationsAssembly));
                options.EnableSensitiveDataLogging(true);

            });

        }

        public static void ConfigureRepositoryService(this IServiceCollection services)
        {
            services.AddSingleton<OrderChangeNotificationRepository>();
            services.AddSingleton<TradeChangeNotificationRepository>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<TradeRepository>();
        }
    }
}
