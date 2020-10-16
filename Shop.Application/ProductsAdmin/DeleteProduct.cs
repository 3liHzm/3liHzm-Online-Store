using System.Threading.Tasks;
using Shop.Domain.Infrastructure;

namespace Shop.Application.ProductsAdmin
{
    [Service]
    public class DeleteProduct
    {
        private readonly IProductManager _productManager;

        public DeleteProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        //method to craete product
        public async Task<bool> Do(int id)
        {
         return await _productManager.DeleteProduct(id) > 0;

        }



    }
}
