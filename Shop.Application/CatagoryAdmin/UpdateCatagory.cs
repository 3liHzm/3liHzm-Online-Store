using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.CatagoryAdmin
{
    [Service]
   public class UpdateCatagory
    {
        private readonly ICatagoryManager _catagoryManager;

        public UpdateCatagory(ICatagoryManager catagoryManager)
        {
            _catagoryManager = catagoryManager;
        }

        public async Task<int> Do(Request request)
        {
            var catagory = new Catagories
            {
                Id = request.Id,
                Catagory = request.Catagory
            };

            return await _catagoryManager.UpdateCatagory(catagory);

        }



        public class Request 
        {
            public int Id { get; set; }
            public string Catagory { get; set; }

        }
    }
}
