using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
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
                string fileName = CreatImgRef() + request.File.FileName;
                string fullPath = Path.Combine(uploads, fileName);
                request.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                product.ImgUrl = fileName;

            }
            


            //Toooo
            if (request.CatagoryId !=0)
            {
            product.CategoryId = request.CatagoryId;

            }

            static string CreatImgRef()  //for uniqe ImgUrls
            {
                var chars = "JHFGJK86789755liopjFfgfgGJKhf54hgh19ss7j4nz7J38CM8kGHJJjHHJFUKFLKkjhfFkfGHKfdKDkGDktKgfjHjGlPlcPjGV4546F35";
                var result = new char[12];
                var random = new Random();

          
                    for (int i = 0; i < result.Length; i++)
                    {
                        result[i] = chars[random.Next(chars.Length)];
                    }
                        return new string(result);
            }



            if (request.Files != null)
            {
                List<ImgGallary> imges =new List<ImgGallary>();
                foreach (var img in request.Files)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"gallery");
                    string fileName = CreatImgRef()+img.FileName;
                    string fullPath = Path.Combine(uploads, fileName);
                    img.CopyTo(new FileStream(fullPath, FileMode.Create));
                    var imgGallery = new ImgGallary
                    {
                        GallaryImgUrl = fileName,
                        ProductId = product.Id
                    };
                    imges.Add(imgGallery);
                }
                    await _productManager.UpdateGallery(imges);
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
            public IEnumerable<IFormFile> Files { get; set; }
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
