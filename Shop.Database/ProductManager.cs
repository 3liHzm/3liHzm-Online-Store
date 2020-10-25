using Microsoft.EntityFrameworkCore;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Database
{
    public class ProductManager : IProductManager
    {
        private readonly ApplicationDbContext _ctx;

        public ProductManager(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<int> CreatProduct(Product product)
        {
            _ctx.Products.Add(product);

            return _ctx.SaveChangesAsync();
        }

        public Task<int> DeleteProduct(int id)
        {
            var prod = _ctx.Products.SingleOrDefault(s => s.Id == id);
            _ctx.Products.Remove(prod);
            return _ctx.SaveChangesAsync();
        }

        public TResult GetProductById<TResult>(int id, Func<Product, TResult> selector)
        {
            return _ctx.Products.Where(s => s.Id == id)
                .Select(selector).FirstOrDefault();
        }

        public TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector)
        {
            return _ctx.Products.Include(s => s.Stock)
                .Include(s=>s.ImgGallary)
                .Where(s => s.Name == name)
                .Select(selector).FirstOrDefault();
        }

        public IEnumerable<TResult> GetProductsByCatagory<TResult>(int catagoryId, Func<Product, TResult> selector)
        {
            return _ctx.Products.Include(s => s.Stock).Where(s => s.CategoryId == catagoryId)
                .Select(selector).ToList();
        }

        public IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector)
        {
            return _ctx.Products
                .Include(s => s.Stock)
                .Select(selector).ToList();
        }

        public IEnumerable<TResult> Search<TResult>(string term, Func<Product, TResult> selector)
        {
            if (term == null)
            {
                return null;
            }
            return _ctx.Products
                .Include(s => s.Stock)
                .Where(s => s.Name.ToLower().Contains(term.ToLower()) || s.Description.ToLower().Contains(term.ToLower()))
                .Select(selector).ToList();
            
        }

        public Task<int> UpdateGallery(IEnumerable<ImgGallary> imgGallary)
        {
            var gallaries = _ctx.ProductImgGallary.Select(s=>s).Where(s => s.ProductId == imgGallary.FirstOrDefault().ProductId);
            _ctx.ProductImgGallary.RemoveRange(gallaries);
            _ctx.ProductImgGallary.AddRange(imgGallary);

            return _ctx.SaveChangesAsync();
        }

        public Task<int> UpdatProduct(Product product)
        {
            _ctx.Products.Update(product);
            return _ctx.SaveChangesAsync();
        }

        public Task<int> UploadGallery(ImgGallary imgGallary)
        {
            _ctx.ProductImgGallary.Add(imgGallary);

            return _ctx.SaveChangesAsync();
        }
    }
}
