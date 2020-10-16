using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Infrastructure;
using System.IO;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    [Service]
    public class UpdateProduct
    {
        private readonly IProductManager _productManager;
        private readonly IHostingEnvironment _hosting;

        public UpdateProduct(IProductManager productManager, IHostingEnvironment hosting)
        {
            _productManager = productManager;
            _hosting = hosting;
        }

        //method to craete product
        public async Task<Response> Do(Request request)
        {

            var product = _productManager.GetProductById(request.Id, s=>s);

            product.Name = request.Name;
            product.Description = request.Description;
            product.Value = request.Value;
            
            if (request.File != null)
            {
                string uploads = Path.Combine(_hosting.WebRootPath, @"images");
                string fullPath = Path.Combine(uploads, request.File.FileName);
                request.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                product.ImgUrl = request.File.FileName;

            }
            //Toooo
            if (request.CatagoryId !=0)
            {
            product.CategoryId = request.CatagoryId;

            }

           await _productManager.UpdatProduct(product);


            return new Response {
                
                      Id = product.Id,
                      Name = product.Name,
                      Description = product.Description,
                      Value = product.Value,
                      ImgUrl = product.ImgUrl

            };
        }

        public class Request
        {
            public int Id { get; set; }
            public int CatagoryId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public IFormFile File { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string ImgUrl { get; set; }
        }
    }
}
