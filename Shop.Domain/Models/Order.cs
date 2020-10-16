﻿using Shop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
   public class Order
    {
        public int Id { get; set; }
        public string OrderRef { get; set; }

        public string CustomerPaymentRef { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }

        public OrderStatus Status { get; set; }

        public ICollection<OrderStock> OrderStocks { get; set; }
    }
}

                                                                 