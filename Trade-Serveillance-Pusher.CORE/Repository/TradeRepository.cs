using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade_Serveillance_Pusher.CORE.Context;
using Trade_Serveillance_Pusher.CORE.Entity;
using Trade_Serveillance_Pusher.CORE.Interfaces;

namespace Trade_Serveillance_Pusher.CORE.Repository
{
   public class TradeRepository
    {
        private readonly TradeServeillanceDbContext context;
        private readonly IHubContext<BroadcastHub, IHubClient> hubContext;

        public TradeRepository(TradeServeillanceDbContext context, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            this.context = context;
            this.hubContext = hubContext;
        }
        public async Task<List<EcxTrade>> GetListOfTrade()
        {
            try
            {
                return await context.EcxTrade.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EcxTrade> GetTrade(string id)
        {
            var Trade = await context.EcxTrade.FindAsync(id);
            return Trade;
        }
        public async Task<bool> PutTrade(Guid id, EcxTrade trade)
        {
            try
            {
                if (id != trade.TradeId)
                {
                    return false;
                }

                context.Entry(trade).State = EntityState.Modified;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TradeExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        public async Task<bool> PostTrade(EcxTrade trade)
        {
            trade.TradeId = Guid.NewGuid();
            context.EcxTrade.Add(trade);


            try
            {
                await context.SaveChangesAsync();
                await hubContext.Clients.All.BroadcastMessage();
            }
            catch (DbUpdateException)
            {
                if (TradeExists(trade.TradeId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        public async Task<bool> DeleteTrade(Guid id)
        {
            var trade = await context.EcxTrade.FindAsync(id);
            if (trade == null)
            {
                return false;
            }


            context.EcxTrade.Remove(trade);

            await context.SaveChangesAsync();
            await hubContext.Clients.All.BroadcastMessage();

            return true;
        }
        private bool TradeExists(Guid id)
        {
            return context.EcxTrade.Any(e => e.TradeId == id);
        }
    }
}
