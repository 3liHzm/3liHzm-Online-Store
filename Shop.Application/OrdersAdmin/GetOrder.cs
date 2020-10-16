using Shop.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.OrdersAdmin
{
    [Service]
    public class GetOrder
    {
        private readonly IOrderManager _orderManager;

        public GetOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }


        public class Response
        {
            public int Id { get; set; }
            public string OrderRef { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }

            public IEnumerable<Product> Products { get; set; }

            public string TotalValue { get; set; }
        }

        public class Product
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string Tvalue { get; set; }
            public int Qty { get; set; }
            public string StockDesc { get; set; }
        }

        public Response Do(int id)
        {
            return _orderManager.GetOrderById(id, s => new Response
            {
                Id = s.Id,
                OrderRef = s.OrderRef,

                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                City = s.City,
                Address1 = s.Address1,
                Address2 = s.Address2,

                Products = s.OrderStocks.Select(x => new Product
                {
                    Name = x.Stock.Product.Name,
                    Description = x.Stock.Product.Description,
                    Value = x.Stock.Product.Value,
                    Tvalue = $"${(x.Stock.Product.Value * x.Qty).ToString("N2")}",
                    Qty = x.Qty,
                    StockDesc = x.Stock.Description,
                }),
                TotalValue = $"${ s.OrderStocks.Sum(x => x.Stock.Product.Value * x.Qty).ToString("N2") }",

            });

        }

    }
}
