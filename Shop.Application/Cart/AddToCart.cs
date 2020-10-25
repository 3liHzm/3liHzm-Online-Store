using Shop.Domain.Infrastructure;

using Shop.Domain.Models;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    [Service]
    public class AddToCart
    {
        private readonly ISessionManager _sessionManager;
        private readonly IStockManager _stockManager;

        public AddToCart(ISessionManager session, IStockManager stockManager)
        {
            _sessionManager = session;
            _stockManager = stockManager;
        }

        public class Request
        {
           
            public int StockId { get; set; }
            public int Qty { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            
            if(!_stockManager.EnoughStock(request.StockId, request.Qty))
            {
                return false;
                //notEnough stock
            }

            await _stockManager.PutStockOnHold(request.StockId, request.Qty, _sessionManager.GetId());

            var stock = _stockManager.GetStockProduct(request.StockId); 
            var cartProduct = new CartProduct()
            {
                ProductName = stock.Product.Name,
                ProductId = stock.ProductId,
                StockId = stock.Id,
                Qty =request.Qty,
                TheValue = stock.Product.Value,
                ImgUrl = stock.Product.ImgUrl 
                
            };
            _sessionManager.AddProduct(cartProduct);

            return true;
            
        }
    }
}
