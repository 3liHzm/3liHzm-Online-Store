using Shop.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;


namespace Shop.Application.Cart
{
    [Service]
    public class GetOrder
    {
        private readonly ISessionManager _sessionManager;

        public GetOrder(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
         
        }


        public class Response
        {
            public IEnumerable<Product> Products { get; set; }
            public CustomerInfo CustomerInfo { get; set; }

            public int GetCharge() => Products.Sum(s => s.Value * s.Qty); // the total price of the cart

        }


        public class Product
        {

            public int ProductId { get; set; }
            public int Qty { get; set; }
            public int StockId { get; set; }
            public int Value { get; set; }
        }


        public class CustomerInfo
        {

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }

            public string PhoneNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
        }
        public Response Do()
        {

            var listOfProducts = _sessionManager.GetCart(s => new Product
            {
                ProductId = s.ProductId,
                StockId = s.StockId,
                Value = (int)(s.TheValue * 100),
                Qty = s.Qty

            });
            
            var customerInfo = _sessionManager.GetCustomerInfo();

            return new Response
            {
                Products = listOfProducts,
                CustomerInfo = new CustomerInfo
                {
                    FirstName = customerInfo.FirstName,
                    LastName = customerInfo.LastName,
                    Email = customerInfo.Email,
                    PhoneNumber = customerInfo.PhoneNumber,
                    Address1 = customerInfo.Address1,
                    Address2 = customerInfo.Address2,
                    City = customerInfo.City
                }

            };


        }
    }
}
