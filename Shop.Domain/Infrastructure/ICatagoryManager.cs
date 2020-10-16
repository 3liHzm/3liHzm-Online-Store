using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure
{
   public interface ICatagoryManager
    {
        Task<int> CreateCatagory(Catagories catagory);
        Task<int> UpdateCatagory(Catagories catagory);
        Task<int> DeleteCatagory(int catagoryId);
        IEnumerable<TResult> GetCatagories<TResult>(Func<Catagories, TResult> selector);
    }
}
