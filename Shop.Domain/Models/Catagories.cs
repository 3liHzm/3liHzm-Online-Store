using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
  public class Catagories
    {
        public int Id { get; set; }
        public string Catagory { get; set; }
        public ICollection<Product> products { get; set; }
    }
}
