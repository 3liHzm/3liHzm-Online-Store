using Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.CatagoryAdmin
{
    [Service]
   public class DeleteCatagory
    {
        private readonly ICatagoryManager _catagoryManager;

        public DeleteCatagory(ICatagoryManager catagoryManager)
        {
            _catagoryManager = catagoryManager;
        }
        public async Task<int> Do(int catagoryId)
        {

            return await _catagoryManager.DeleteCatagory(catagoryId);

        }
    }
}
