using Shop.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.StockAdmin
{
    [Service]
    public class GetStocks
    {
        private readonly IProductManager _productManager;

        public GetStocks(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Do()      
        {
            return _productManager.GetProductsWithStock(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Value = $"${ s.Value.ToString("N2") }",
                Stock = s.Stock.Select(x => new StockViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Qty = x.Qty,

                })
            }).Reverse();


             
        }
        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public decimal Qty { get; set; }
          
        }
        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }
}
