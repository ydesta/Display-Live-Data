using Microsoft.AspNetCore.Builder;
using Trade_Serveillance_Pusher.CORE.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace Trade_Serveillance_Pusher.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSqlTableDependency<T>(this IApplicationBuilder services, string connectionString)
            where T : ISqlTableDependencyRepository
        {
            var serviceProvider = services.ApplicationServices;
            var subscription = serviceProvider.GetService<T>();
            subscription.Configure(connectionString);
        }
    }
}
