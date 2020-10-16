using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;

namespace Shop.Application.ProductsAdmin
{
    [Service]
    public class CreateProduct
   {
        private readonly IProductManager _productManager;
        private readonly IHostingEnvironment _hosting;

        public CreateProduct(IProductManager productManager, IHostingEnvironment hosting)
        {
            _productManager = productManager;
            _hosting = hosting;
        }

        //method to craete product
        public async Task<Response> Do(Request request)
        {

            //ToDo adding image(s)
            if (request.File != null)
            {
                string uploads = Path.Combine(_hosting.WebRootPath, @"images");
                string fullPath = Path.Combine(uploads, request.File.FileName);
                request.File.CopyTo(new FileStream(fullPath, FileMode.Create));
            }


            var product = new Product
            {

                Name = request.Name,
                Description = request.Description,
                Value = request.Value,
                ImgUrl = request.File.FileName,
                //Tooo
                CategoryId = request.CatagoryId

                };

           if(await _productManager.CreatProduct(product) <= 0)
            {
                throw new Exception("Faild To Creat Product");
            }

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value,
                ImgUrl = product.ImgUrl,
                //Tooo
                CatagoryId = product.CategoryId


            };
        }

        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public int CatagoryId { get; set; }
            // public string ImgUrl { get; set; }
            public IFormFile File { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public int CatagoryId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string ImgUrl { get; set; }

        }


    }
}
