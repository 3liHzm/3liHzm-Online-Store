using Shop.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Infrastructure;
using System.Collections.Generic;

namespace Shop.Database
{

    public class StockManager : IStockManager
        {
            private readonly ApplicationDbContext _ctx;
            public StockManager(ApplicationDbContext ctx)
            {
                _ctx = ctx;
            }

        public Task<int> CreateStock(Stock stock)
        {
            _ctx.Stock.Add(stock);

            return _ctx.SaveChangesAsync();
        }

        public Task<int> DeleteStock(int id)
        {
            var stock = _ctx.Stock.SingleOrDefault(s => s.Id == id);
            _ctx.Stock.Remove(stock);
            return _ctx.SaveChangesAsync();
        }
        public Task<int> UpdateStockRange(List<Stock> stocks)
        {

            _ctx.Stock.UpdateRange(stocks);
            return _ctx.SaveChangesAsync();
        }

        public bool EnoughStock(int stockId, int qty)
            {
                if(stockId == 0)
                {
                    return false;
                }
                return _ctx.Stock.FirstOrDefault(s => s.Id == stockId).Qty>=qty;
            }

            public Stock GetStockProduct(int stockId)
            {
               return _ctx.Stock
                .Include(s => s.Product)
                .FirstOrDefault(s => s.Id == stockId);
            }

            public Task PutStockOnHold(int stockId, int qty, string sessionId)
            {
                _ctx.Stock.FirstOrDefault(s => s.Id == stockId).Qty -= qty;


                var stockOnHold = _ctx.StockOnHolds
                    .Where(s => s.SessionId == sessionId).ToList();

                if (stockOnHold.Any(s => s.StockId == stockId))
                {
                    stockOnHold.Find(s => s.StockId == stockId).Qty += qty;
                }
                else
                {
                    _ctx.StockOnHolds.Add(new StockOnHold
                    {
                        StockId = stockId,
                        SessionId = sessionId,
                        Qty = qty,
                        Exp = DateTime.Now.AddMinutes(15)

                    });


                }
                foreach (var stock in stockOnHold)
                {
                    stock.Exp = DateTime.Now.AddMinutes(15);
                }


                return _ctx.SaveChangesAsync();
            }

            public Task RemoveStockFromHold(int stockId, int qty, string sessionId)
            {
                var stockOnHold = _ctx.StockOnHolds.FirstOrDefault(s => s.SessionId == sessionId && s.StockId == stockId);

                var stock = _ctx.Stock.FirstOrDefault(s => s.Id == stockId);

                stock.Qty += qty;
                stockOnHold.Qty -= qty;
                if (stockOnHold.Qty <= 0)
                {
                    _ctx.Remove(stockOnHold);

                }
                return _ctx.SaveChangesAsync();

            }

        public Task RemoveStockFromHold(string sessionId)
        {
            var stockOnHold = _ctx.StockOnHolds.AsEnumerable()
                .Where(s => s.SessionId == sessionId).ToList();

            _ctx.StockOnHolds.RemoveRange(stockOnHold);
            return _ctx.SaveChangesAsync();
        }

        public Task ReturnBackStockOnHold()
        {
            var expStockOnHold = _ctx.StockOnHolds.Where(s => s.Exp < DateTime.Now).ToList();

            if (expStockOnHold.Count > 0)
            {
                var stockToReturn = _ctx.Stock.AsEnumerable().Where(s => expStockOnHold.Any(x => x.StockId == s.Id)).ToList();

                foreach (var stock in stockToReturn)
                {
                    stock.Qty = stock.Qty + expStockOnHold.FirstOrDefault(s => s.StockId == stock.Id).Qty;
                }

                _ctx.StockOnHolds.RemoveRange(expStockOnHold);

                return _ctx.SaveChangesAsync();
            }
            return Task.CompletedTask;

        }


    }
}
