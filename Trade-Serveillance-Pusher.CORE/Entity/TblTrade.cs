using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade_Serveillance_Pusher.CORE.Entity
{
   public class TblTrade
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public Guid BuyOrderTicketId { get; set; }
        public Guid SellOrderTicketId { get; set; }
        public byte StatusId { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public DateTime TradedTimestamp { get; set; }
        public byte WRSelectionMethodId { get; set; }
        public Guid WRId { get; set; }
        public byte TradeTypeId { get; set; }
        public int ProductionYear { get; set; }
        public bool IsOnline { get; set; }
        public string Remark { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public DateTime LastUpdatedTimestamp { get; set; }
        public string ChangeType { get; set; }
    }
}
