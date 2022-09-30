using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade_Serveillance_Pusher.CORE.Entity
{
    public class EcxTrade
    {
        [Key]
        public Guid TradeId { get; set; }
        public string Warehouse { get; set; }
        public string CommodityGrade { get; set; }
        public decimal? OrderQuantity { get; set; }
        public decimal? OrderPrice { get; set; }
        public decimal? TradeQuantity { get; set; }
        public decimal? TradePrice { get; set; }
        public DateTime? SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }
        public int ProductionYear { get; set; }
        public string CommodityType { get; set; }
        public string CommodityClass { get; set; }
        public bool IsVoilation { get; set; }
        public string BuyerRepName { get; set; }
        public string SellerRepName { get; set; }
        public double? Lot { get; set; }
        public double? Quintal { get; set; }
        public double? Bags { get; set; }
        public double? VolumeKGUsingSTD { get; set; }
        public string ConsignmentType { get; set; }
        public DateTime? TradedTimestamp { get; set; }
        public string SellerClientName { get; set; }
        public string BuyerClientName { get; set; }
        public string SellerMember { get; set; }
        public string BuyerMember { get; set; }
        public string Symbol { get; set; }
        public string ViewStatus { get; set; }
        public string BuyerMemberId { get; set; }
        public string SellerMemberId { get; set; }
        public Guid? CommodityGradeId { get; set; }
        public Guid? WarehouseId { get; set; }
        public Guid? SessionId { get; set; }
        public int? BuyerMemberClass { get; set; }
        public int? SellerMemberClass { get; set; }
        public bool? IsBuyerClientTrade { get; set; }
        public bool? IsSellerClientTrade { get; set; }
        public decimal? BuyerQuantity { get; set; }
        public decimal? BuyerPrice { get; set; }
        public long? WRNo { get; set; }
        public double? TradeWarehouseRecieptQuantity { get; set; }
        public Guid? CommodityId { get; set; }
        public double? TradedWRQuantity { get; set; }
    }


}
