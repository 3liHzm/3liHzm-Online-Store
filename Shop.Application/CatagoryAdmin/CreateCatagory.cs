using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.CatagoryAdmin
{
    [Service]
   public class CreateCatagory
    {
        private readonly ICatagoryManager _catagoryManager;

        public CreateCatagory(ICatagoryManager catagoryManager)
        {
            _catagoryManager = catagoryManager;
        }
        public async Task<Response> Do(Request requset)
        {
            var catagory = new Catagories
            {
                Catagory = requset.Catagory
            };

            await _catagoryManager.CreateCatagory(catagory);
           
            return new Response
            {
              Id = catagory.Id,
              Catagory = catagory.Catagory
            };

        }


        public class Request
        {
           public string Catagory { get; set; }

        }
        public class Response
        {
            public int Id { get; set; }
            public string Catagory { get; set; }
        }
    }
}
