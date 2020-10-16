using Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Application.CatagoryAdmin
{
    [Service]
    public class GetCatagories
    {
        private readonly ICatagoryManager _catagoryManager;

        public GetCatagories(ICatagoryManager catagoryManager)
        {
            _catagoryManager = catagoryManager;
        }

        public IEnumerable<CategoriesViewModel> Do()
        {
           return _catagoryManager.GetCatagories(s => new CategoriesViewModel
            {
               Id = s.Id,
               Catagory = s.Catagory
            }); 
        }
    }

    public class CategoriesViewModel
    {
        public int Id { get; set; }
        public string Catagory { get; set; }
    }
}
