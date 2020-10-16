using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    [Service]
    public class CreateStock
    {
        private readonly IStockManager _stockManager;

        public CreateStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }  
        


        public async Task<Response> Do(Request requset)
        {
           var stock = new Stock
            {
                Description = requset.Description,
                Qty = requset.Qty,
                ProductId = requset.ProductId
            };

           await _stockManager.CreateStock(stock);

            return new Response
            {
                Id = stock.Id,
                Description = stock.Description,
                Qty = stock.Qty
            };
        }
        public class Request
        {
            public string Description { get; set; }
            public int Qty { get; set; }

            public int ProductId { get; set; }
        }  

        public class Response
        {

            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }

            
        }

    }
}
