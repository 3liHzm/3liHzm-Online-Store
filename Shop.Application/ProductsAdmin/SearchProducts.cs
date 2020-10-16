using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.ProductsAdmin
{
    [Service]
   public class SearchProducts
    {
        private readonly IProductManager _productManager;

        public SearchProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductAdminViewModel> Do(string term )
        {
            return _productManager.Search(term, s => new ProductAdminViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Value = s.Value,
                 Stock = s.Stock.Select(x => new StockViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Qty = x.Qty,

                })

            });
           
        }

       public class ProductAdminViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public decimal Qty { get; set; }

        }

    }
}
