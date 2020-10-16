using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
   public class Stock
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
       
        public int ProductId { get; set; } //Fk
        public Product Product { get; set; }
        public ICollection<OrderStock> OrderStocks { get; set; }

    }
}
