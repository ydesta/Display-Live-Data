using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade_Serveillance_Pusher.CORE.Entity;

namespace Trade_Serveillance_Pusher.CORE.Context
{
  public class TradeServeillanceDbContext : DbContext
    {
        public TradeServeillanceDbContext(DbContextOptions<TradeServeillanceDbContext> options): base(options)
        {

        }
        public DbSet<EcxTrade> EcxTrade { get; set; }
        public DbSet<EcxOrder> EcxOrder { get; set; }
        public DbSet<TblOrder> TblOrder { get; set; }
        public DbSet<TblTrade> TblTrade { get; set; }
        public DbSet<LogMessage> LogMessage { get; set; }
    }
}
