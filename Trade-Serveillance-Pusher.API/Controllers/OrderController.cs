using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trade_Serveillance_Pusher.CORE.Entity;
using Trade_Serveillance_Pusher.CORE.Repository;

namespace Trade_Serveillance_Pusher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _repository;

        public OrderController(OrderRepository repository)
        {
            _repository = repository;
        }
        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EcxOrder>>> GetListOfOrder()
        {
            return await _repository.GetListOfOrder();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EcxOrder>> GetOrderById(string id)
        {
            return await _repository.GetOrder(id);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<bool> PutTrade(Guid id, EcxOrder order)
        {
            return await _repository.PutOrder(id, order);
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<bool> PostOrder(EcxOrder order)
        {
            return await _repository.PostOrder(order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteOrder(Guid id)
        {
            return await _repository.DeleteOrder(id);
        }
    }
}
