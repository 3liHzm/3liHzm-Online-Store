using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Database
{
   public class CatagoryManager : ICatagoryManager
    {
        private readonly ApplicationDbContext _ctx;
        public CatagoryManager(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<int> CreateCatagory(Catagories catagory)
        {
            _ctx.Categories.Add(catagory);

            return _ctx.SaveChangesAsync();
        }

        public Task<int> DeleteCatagory(int catagoryId)
        {
            var category = _ctx.Categories.SingleOrDefault(s => s.Id == catagoryId);
            _ctx.Categories.Remove(category);
            return _ctx.SaveChangesAsync();
        }

        public IEnumerable<TResult> GetCatagories<TResult>(Func<Catagories, TResult> selector)
        {
            return _ctx.Categories
                .Select(selector).ToList();
        }

        public Task<int> UpdateCatagory(Catagories catagory)
        {
            _ctx.Categories.Update(catagory);
            return _ctx.SaveChangesAsync();
        }
    }
}
