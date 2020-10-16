using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.ViewModels
{
    public class ProductViewModel
    {
        public int CatagoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string ImgUrl { get; set; }
        public int StockCount { get; set; }
    }
}
