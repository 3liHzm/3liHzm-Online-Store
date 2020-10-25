using Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Products
{
    [Service]
    public class GetProductsByCatagory
    {
        private readonly IProductManager _productManager;

        public GetProductsByCatagory(IProductManager productManager)
        {
            _productManager = productManager;

        }


        public IEnumerable<ProductViewModel> Do(int catagoryId)
        {
                        
            return _productManager.GetProductsByCatagory(catagoryId, s => new ProductViewModel //we ganna brig it from db than put it in the viwemodel? igus
            {
                Name = s.Name,
                Description = s.Description,
                ImgUrl = s.ImgUrl,
                StockCount = s.Stock.Sum(x => x.Qty),
                CatagoryId = s.CategoryId ,
                Value = $"${ s.Value.ToString("N2") }"

            });

        }
        public class ProductViewModel
        {
            public int CatagoryId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string ImgUrl { get; set; }
            public string Value { get; set; }
            public int StockCount { get; set; }

        }
    }
}
