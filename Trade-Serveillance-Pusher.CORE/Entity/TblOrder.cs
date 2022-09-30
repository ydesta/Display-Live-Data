using System;
using System.ComponentModel.DataAnnotations;

namespace Trade_Serveillance_Pusher.CORE.Entity
{
    public class TblOrder
    {
        [Key]
        public Guid Id { get; set; }
        public string OrderId { get; set; }
        public Guid SessionId { get; set; }
        public Guid CommodityGradeId { get; set; }
        public decimal Quantity { get; set; }
        public decimal LimitPrice { get; set; }
        public Guid MemberId { get; set; }
        public Guid RepId { get; set; }
        public bool IsClientOrder { get; set; }
        public Guid ClientId { get; set; }
        public Guid WarehouseId { get; set; }
        public int? ProductionYear { get; set; }
        public byte TransactionType { get; set; }
        public byte OrderType { get; set; }
        public byte OrderValidityId { get; set; }
        public DateTime ValidityDate { get; set; }
        public byte FillTypeId { get; set; }
        public byte OrderStatusId { get; set; }
        public DateTime? SubmittedTimestamp { get; set; }
        public DateTime? ReceivedTimestamp { get; set; }
        public DateTime? ExecutedTimestamp { get; set; }
        public DateTime? CancelledTimestamp { get; set; }
        public bool IsIF { get; set; }
        public Guid? WRId { get; set; }
        public int? TradingCenterId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedTimestamp { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedTimestamp { get; set; }
        public DateTime? SubmittedTimestamp2 { get; set; }
        public DateTime? ReceivedTimestamp2 { get; set; }
        public DateTime? LastUpdatedTimestamp2 { get; set; }
        public string SubmittedTicks { get; set; }
        public string UpdatedTicks { get; set; }
        public string ChangeType { get; set; }
    }
}
