using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade_Serveillance_Pusher.CORE.Entity
{
    public class EcxOrder
    {
        [Key]
        public Guid OrderId { get; set; }
        public string CommodityType { get; set; }
        public string CommodityClass { get; set; }
        public string CommodityGrade { get; set; }
        public int? ProductionYear { get; set; }
        public string OrderType { get; set; }
        public string Warehouse { get; set; }
        public string Member { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public DateTime? SubmittedTimestamp { get; set; }
        public string RepName { get; set; }
        public string ClientName { get; set; }
        public bool IsViolation { get; set; }
        public string Symbol { get; set; }
        public string OrderStatus { get; set; }
        public string ViewStatus { get; set; }
        public string MemberId { get; set; }
        public Guid? SessionId { get; set; }
        public DateTime? SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }
        public Guid? CommodityGradeId { get; set; }
        public Guid? WarehouseId { get; set; }
        public byte OrderStatusId { get; set; }
        public DateTime? CancelledTimestamp { get; set; }
        public bool? IsClientOrder { get; set; }
        public Byte? TransactionType { get; set; }

    }


}
