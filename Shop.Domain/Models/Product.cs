using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace Shop.Domain.Models
{
  public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public decimal Value { get; set; }

        public int CategoryId { get; set; }
        public Catagories Categories { get; set; }

        public ICollection<Stock> Stock { get; set; }

    }
}
