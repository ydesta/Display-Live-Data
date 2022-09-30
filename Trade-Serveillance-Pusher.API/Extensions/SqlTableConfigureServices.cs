using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Trade_Serveillance_Pusher.CORE.Repository.TableDependency;

namespace Trade_Serveillance_Pusher.API.Extensions
{
    public static class SqlTableConfigureServices
    {
        public static void UseSqlTableConfigureService(this IApplicationBuilder services, IConfiguration config)
        {
            string ecxConnectionString = config.GetConnectionString("ECXConnection");
            string eceaConnectionString = config.GetConnectionString("DefaultConnection");

            services.UseSqlTableDependency<OrderChangeNotificationRepository>(ecxConnectionString);
            services.UseSqlTableDependency<TradeChangeNotificationRepository>(eceaConnectionString);
        }
    }
}
