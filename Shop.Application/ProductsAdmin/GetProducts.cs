using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System.Collections.Generic;

namespace Shop.Application.ProductsAdmin
{
    [Service]
    public class GetProducts     //we didn't use this code 
    {
        private readonly IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Do()
        {
            return _productManager.GetProductsWithStock(s => new ProductViewModel 
            {
                Id = s.Id,
                Name = s.Name,
                Value = s.Value,
                Stocks = s.Stock
                
            });

        }
        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
            public ICollection<Stock> Stocks { get; set; }
    }
    }

}
