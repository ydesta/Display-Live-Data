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
    public class TradeController : ControllerBase
    {
        private readonly TradeRepository _repository;

        public TradeController(TradeRepository repository)
        {
            _repository = repository;
        }
        // GET: api/Trades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EcxTrade>>> GetListOfTrade()
        {
            return await _repository.GetListOfTrade();
        }

        // GET: api/Trades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EcxTrade>> GetTradeById(string id)
        {
            return await _repository.GetTrade(id);
        }

        // PUT: api/Trades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<bool> PutTrade(Guid id, EcxTrade trade)
        {
            return await _repository.PutTrade(id, trade);
        }

        // POST: api/trades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<bool> PostTrade(EcxTrade trade)
        {
            return await _repository.PostTrade(trade);
        }

        // DELETE: api/Trades/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteTrade(Guid id)
        {
            return await _repository.DeleteTrade(id);
        }
    }
}
