using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Trade_Serveillance_Pusher.API.Extensions;
using Trade_Serveillance_Pusher.CORE;
using Trade_Serveillance_Pusher.CORE.Repository.TableDependency;

namespace Trade_Serveillance_Pusher.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.ConfigureIISIntegration();

            services.ConfigureSwagger();
            services.ConfigureCors();
            services.AddSignalR();
            services.ConfigureSqlContext(Configuration);
            services.AddControllers();
            services.ConfigureRepositoryService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trade_Serveillance_Pusher.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<BroadcastHub>("/notify");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // For Database change notification
            app.UseSqlTableConfigureService(Configuration);
        }
    }
}
