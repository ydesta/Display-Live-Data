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
    public class TradeChangeNotificationRepository : ISqlTableDependencyRepository
    {
        private bool disposedValue = false;
        private SqlTableDependency<TblTrade> _tableDependency;
        private readonly IHubContext<BroadcastHub, IHubClient> hubContext;
        public IConfiguration _configuration { get; }
        private TradeServeillanceDbContext _context;
        public TradeChangeNotificationRepository(IHubContext<BroadcastHub, IHubClient> hubContext, IConfiguration configuration)
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
                _tableDependency = new SqlTableDependency<TblTrade>(connectionString, null, null, null, null, null, DmlTriggerType.All);
                _tableDependency.OnChanged += Changed;
                _tableDependency.OnError += OnError;
                _tableDependency.Start();
            }
            catch (Exception ex)
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

        private void Changed(object sender, RecordChangedEventArgs<TblTrade> e)
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
                    //Brodcast the changed entity
                    hubContext.Clients.All.BroadcastMessage();
                }
            }
            catch (Exception ex)
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
        public void SaveEcxOrder(TblTrade ecxTrade, string changeType)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            var optionBuilder = new DbContextOptionsBuilder<TradeServeillanceDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            _context = new TradeServeillanceDbContext(optionBuilder.Options);
            try
            {
                TblTrade newTrade = new TblTrade()
                {
                    BuyOrderTicketId = ecxTrade.BuyOrderTicketId,
                    CreatedBy = ecxTrade.CreatedBy,
                    CreatedTimestamp = ecxTrade.CreatedTimestamp,
                    Id = ecxTrade.Id,
                    IsOnline = ecxTrade.IsOnline,
                    LastUpdatedBy = ecxTrade.LastUpdatedBy,
                    LastUpdatedTimestamp = ecxTrade.LastUpdatedTimestamp,
                    Price = ecxTrade.Price,
                    ProductionYear = ecxTrade.ProductionYear,
                    Quantity = ecxTrade.Quantity,
                    Remark = ecxTrade.Remark,
                    SellOrderTicketId = ecxTrade.SellOrderTicketId,
                    SessionId = ecxTrade.SessionId,
                    StatusId = ecxTrade.StatusId,
                    TradedTimestamp = ecxTrade.TradedTimestamp,
                    TradeTypeId = ecxTrade.TradeTypeId,
                    WRId = ecxTrade.WRId,
                    WRSelectionMethodId = ecxTrade.WRSelectionMethodId,
                    ChangeType = changeType
                };
                _context.TblTrade.Add(newTrade);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region IDisposable

        ~TradeChangeNotificationRepository()
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
