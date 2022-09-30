using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Linq;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using Trade_Serveillance_Pusher.CORE.Context;
using Trade_Serveillance_Pusher.CORE.Entity;
using Trade_Serveillance_Pusher.CORE.Interfaces;

namespace Trade_Serveillance_Pusher.CORE.Repository.TableDependency
{
    public class OrderChangeNotificationRepository: ISqlTableDependencyRepository
    {
        private bool disposedValue = false;
        private SqlTableDependency<TblOrder> _tableDependency;
        private readonly IHubContext<BroadcastHub, IHubClient> hubContext;
        public IConfiguration _configuration { get; }
        private TradeServeillanceDbContext _context;
        public OrderChangeNotificationRepository(IHubContext<BroadcastHub, IHubClient> hubContext, IConfiguration configuration)
        {
            this.hubContext = hubContext;
            _configuration = configuration;
        }

        public void Configure(string connectionString)
        {
            string orderConnectionString = _configuration.GetConnectionString("DefaultConnection");
            var optionBuilder = new DbContextOptionsBuilder<TradeServeillanceDbContext>();
            optionBuilder.UseSqlServer(orderConnectionString);
            _context = new TradeServeillanceDbContext(optionBuilder.Options);
            try
            {
                _tableDependency = new SqlTableDependency<TblOrder>(connectionString, null, null, null, null, null, DmlTriggerType.All);
                _tableDependency.OnChanged += Changed;
                _tableDependency.OnError += OnError;
                _tableDependency.Start();
            } catch(Exception ex)
            {              
                var trace = new StackTrace(ex, true);
                var frame = trace.GetFrames().Last();
                LogMessage log = new LogMessage()
                {
                    FileName = frame.GetFileName(),
                    LineNumber = frame.GetFileLineNumber(),
                    CreatedDate = DateTime.Now,
                    Description = "Sql Tablle Dependency Erorr",
                    Message = ex.Message,
                    Success = false,
                };
                _context.LogMessage.Add(log);
                _context.SaveChanges();
            }
           
        }
       
        private void OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"SqlTableDependency error: {e.Error.Message}");
        }

        private void Changed(object sender, RecordChangedEventArgs<TblOrder> e)
        {
            string orderConnectionString = _configuration.GetConnectionString("DefaultConnection");
            var optionBuilder = new DbContextOptionsBuilder<TradeServeillanceDbContext>();
            optionBuilder.UseSqlServer(orderConnectionString);
            _context = new TradeServeillanceDbContext(optionBuilder.Options);
            try
            {
                if (e.ChangeType != ChangeType.None)
                {
                    // TODO: manage the changed entity
                    string type = e.ChangeType.ToString();
                    var changedEntity = e.Entity;
                    SaveEcxOrder(changedEntity, type);
                    hubContext.Clients.All.BroadcastMessage();
                }
            }
            catch(Exception ex)
            {
                var trace = new StackTrace(ex, true);
                var frame = trace.GetFrames().Last();
                LogMessage log = new LogMessage()
                {
                    FileName = frame.GetFileName(),
                    LineNumber = frame.GetFileLineNumber(),
                    CreatedDate = DateTime.Now,
                    Description = "On Change Erorr",
                    Message = ex.Message,
                    Success = false,
                };
                _context.LogMessage.Add(log);
                _context.SaveChanges();
            }
           
        }
        public void SaveEcxOrder(TblOrder ecxOrder,string changeType)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            var optionBuilder = new DbContextOptionsBuilder<TradeServeillanceDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            _context = new TradeServeillanceDbContext(optionBuilder.Options);
            try
            {
                TblOrder newOrder = new TblOrder()
                {
                    Id = ecxOrder.Id,
                    CancelledTimestamp = ecxOrder.CancelledTimestamp,
                    ClientId = ecxOrder.ClientId,
                    CommodityGradeId = ecxOrder.CommodityGradeId,
                    CreatedBy = ecxOrder.CreatedBy,
                    CreatedTimestamp = ecxOrder.CreatedTimestamp,
                    ExecutedTimestamp = ecxOrder.ExecutedTimestamp,
                    FillTypeId = ecxOrder.FillTypeId,
                    IsClientOrder = ecxOrder.IsClientOrder,
                    IsIF = ecxOrder.IsIF,
                    LastUpdatedBy = ecxOrder.LastUpdatedBy,
                    LastUpdatedTimestamp = ecxOrder.LastUpdatedTimestamp,
                    LastUpdatedTimestamp2 = ecxOrder.LastUpdatedTimestamp2,
                    LimitPrice = ecxOrder.LimitPrice,
                    MemberId = ecxOrder.MemberId,
                    OrderId = ecxOrder.OrderId,
                    OrderStatusId = ecxOrder.OrderStatusId,
                    OrderType = ecxOrder.OrderType,
                    OrderValidityId = ecxOrder.OrderValidityId,
                    ProductionYear = ecxOrder.ProductionYear,
                    Quantity = ecxOrder.Quantity,
                    ReceivedTimestamp = ecxOrder.ReceivedTimestamp,
                    ReceivedTimestamp2 = ecxOrder.ReceivedTimestamp2,
                    RepId = ecxOrder.RepId,
                    SessionId = ecxOrder.SessionId,
                    SubmittedTicks = ecxOrder.SubmittedTicks,
                    SubmittedTimestamp = ecxOrder.SubmittedTimestamp,
                    SubmittedTimestamp2 = ecxOrder.SubmittedTimestamp2,
                    TradingCenterId = ecxOrder.TradingCenterId,
                    TransactionType = ecxOrder.TransactionType,
                    UpdatedTicks = ecxOrder.UpdatedTicks,
                    ValidityDate = ecxOrder.ValidityDate,
                    WarehouseId = ecxOrder.WarehouseId,
                    WRId = ecxOrder.WRId,
                    ChangeType = changeType
                };
                _context.TblOrder.Add(newOrder);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
        }
        #region IDisposable

        ~OrderChangeNotificationRepository()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tableDependency.Stop();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
