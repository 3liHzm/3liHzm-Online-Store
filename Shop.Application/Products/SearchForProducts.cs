using Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Shop.Domain.Models;
using System.Linq;

namespace Shop.Application.Products
{
    [Service]
   public class SearchForProducts
    {
        private readonly IProductManager _productManager;

        public SearchForProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public IEnumerable<ProductViewModel> Do(string term)
        {

           return _productManager.Search(term, s=> new ProductViewModel 
           {
            CatagoryId = s.CategoryId,
            Name = s.Name,
            Description =s.Description,
            Value = $"${ s.Value.ToString("N2") }",
            ImgUrl=s.ImgUrl,
            StockCount = s.Stock.Sum(x => x.Qty)

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
