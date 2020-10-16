using Shop.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    [Service]
    public class DeletStock
    {
        private readonly IStockManager _stockManager;

        public DeletStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }


        public async Task<int> Do(int id)
        {

          return await _stockManager.DeleteStock(id);
            
        }
    }
  
}
