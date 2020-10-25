using Shop.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.Products
{
    [Service]
    public class GetProduct
    {
        private readonly IStockManager _stockManager;
        private readonly IProductManager _productManager;


        public GetProduct(
            IStockManager stockManager,
            IProductManager productManager)
        {
            _stockManager = stockManager;
            _productManager = productManager;

        }

        public async Task <ProductViewModel> Do(string name)
        {

            await _stockManager.ReturnBackStockOnHold();

            return  _productManager.GetProductByName(name, s => new ProductViewModel
            {

                Name = s.Name,
                Description = s.Description,
                Value = $"${ s.Value.ToString("N2") }",
                ImgUrl = s.ImgUrl,
                Stock = s.Stock.Select(x => new StockViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Qty = x.Qty,
                }),
                Gallery = s.ImgGallary.Select(x => new GalleryViewModel
                {
                    Id = x.Id,
                    GallaryImgUrl = x.GallaryImgUrl
                })

            });

        }
        public class ProductViewModel
        {
            
            public string Name { get; set; }
            public string Description { get; set; }
            public string ImgUrl { get; set; }
            public string Value { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
            public IEnumerable<GalleryViewModel> Gallery { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }


        }     
        public class GalleryViewModel
        {
            public int Id { get; set; }
            public string GallaryImgUrl { get; set; }

        }
    }
}
