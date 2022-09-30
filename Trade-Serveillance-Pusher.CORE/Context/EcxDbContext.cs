using Microsoft.EntityFrameworkCore;
using Trade_Serveillance_Pusher.CORE.Entity;

namespace Trade_Serveillance_Pusher.CORE.Context
{
    public class EcxDbContext : DbContext
    {
        public EcxDbContext(DbContextOptions<EcxDbContext> options) : base(options)
        {

        }
        public DbSet<TblOrder> TblOrder { get; set; }
        public DbSet<TblTrade> TblTrade { get; set; }
    }
}
