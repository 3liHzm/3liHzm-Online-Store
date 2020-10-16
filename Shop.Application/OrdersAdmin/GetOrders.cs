using Shop.Domain.Enums;
using Shop.Domain.Infrastructure;
using System.Collections.Generic;

namespace Shop.Application.OrdersAdmin
{
    [Service]
    public class GetOrders
    {
        private readonly IOrderManager _orderManager;

        public GetOrders(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public IEnumerable<Response> Do(int status)
        {
            return _orderManager.GetOrdersByStatus((OrderStatus)status, s => new Response
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                PhoneNumber = s.PhoneNumber,
                City = s.City,
            });    
        }
        public class Response
        {

            public int Id { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string City { get; set; }

       
        }
    }
}
