using Shop.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using Shop.Domain.Models;

namespace Shop.Application.Products
{
    [Service]
    public class GetProducts
    {
        private readonly IProductManager _productManager;

        public GetProducts(
            IProductManager productManager)
        {
            _productManager = productManager;

        }

        public IEnumerable<ProductViewModel> Do()
        {

            return _productManager.GetProductsWithStock(s => new ProductViewModel //we ganna brig it from db than put it in the viwemodel? igus
            {
                Name = s.Name,
                Description = s.Description,
                Value = $"${ s.Value.ToString("N2") }",
                ImgUrl = s.ImgUrl,

                StockCount = s.Stock.Sum(x => x.Qty),
                CatagoryId=s.CategoryId

            });
 
        }
        //public class ProductViewModel
        //{
        //    public int CatagoryId { get; set; }
        //    public string Name { get; set; }
        //    public string Description { get; set; }
        //    public string Value { get; set; }
        //    public string ImgUrl { get; set; }
        //    public int StockCount { get; set; }

        //}
    }

}
