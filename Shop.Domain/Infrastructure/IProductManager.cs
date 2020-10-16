using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure
{
    public interface IProductManager
    {
        Task<int> CreatProduct(Product product);
        Task<int> UpdatProduct(Product product);
        Task<int> DeleteProduct(int id);
        public IEnumerable<TResult> Search<TResult>(string term, Func<Product, TResult> selector);
        TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector);
        TResult GetProductById<TResult>(int id, Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductsByCatagory<TResult>(int catagoryId, Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector);
    }
}
