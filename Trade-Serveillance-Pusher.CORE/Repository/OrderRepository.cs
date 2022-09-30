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
    public class OrderRepository
    {
        private readonly TradeServeillanceDbContext context;
        private readonly IHubContext<BroadcastHub, IHubClient> hubContext;

        public OrderRepository(TradeServeillanceDbContext context, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            this.context = context;
            this.hubContext = hubContext;
        }
        public async Task<List<EcxOrder>> GetListOfOrder()
        {
            try
            {
                return await context.EcxOrder.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EcxOrder> GetOrder(string id)
        {
            var order = await context.EcxOrder.FindAsync(id);
            return order;
        }
        public async Task<bool> PutOrder(Guid id, EcxOrder order)
        {
            try
            {
                if (id != order.OrderId)
                {
                    return false;
                }

                context.Entry(order).State = EntityState.Modified;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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
        public async Task<bool> PostOrder(EcxOrder order)
        {
            order.OrderId = Guid.NewGuid();
            context.EcxOrder.Add(order);


            try
            {
                await context.SaveChangesAsync();
                await hubContext.Clients.All.BroadcastMessage();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.OrderId))
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
        public async Task<bool> DeleteOrder(Guid id)
        {
            var order = await context.EcxOrder.FindAsync(id);
            if (order == null)
            {
                return false;
            }


            context.EcxOrder.Remove(order);

            await context.SaveChangesAsync();
            await hubContext.Clients.All.BroadcastMessage();

            return true;
        }
        private bool OrderExists(Guid id)
        {
            return context.EcxOrder.Any(e => e.OrderId == id);
        }

    }
}
