using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    [Service]
    public class UpdateStock
    {
        private readonly IStockManager _stockManager;

        public UpdateStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<Response> Do(Request request)
        {            
            var stocks = new List<Stock>();

            foreach (var stock in request.Stock)
            {
                stocks.Add(new Stock
                {                       
                    Id = stock.Id,
                    Description = stock.Description,
                    Qty = stock.Qty,
                    ProductId = stock.ProductId
                });

            }

            await _stockManager.UpdateStockRange(stocks);


            return new Response
            {
                Stock = request.Stock
            };

        }

        public class StockViwModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }

            public int ProductId { get; set; }
        }

        public class Request
        {
            public IEnumerable<StockViwModel> Stock { get; set; }
        }  
        public class Response
        {
            public IEnumerable<StockViwModel> Stock { get; set; }

        }


    }
}
