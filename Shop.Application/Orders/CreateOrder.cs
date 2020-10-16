using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.Orders
{
    [Service]
    public class CreateOrder
    {
        private readonly IStockManager _stockManager;
        private readonly IOrderManager _orderManager;

        public CreateOrder(IStockManager stockManager,
            IOrderManager orderManager)
        {
            _stockManager = stockManager;
            _orderManager = orderManager;
        }

        public class Request
        {
            
            public string StripeRef { get; set; }   //delete it when payment in 2stlam or change it when u use zainCash
           
            public string SessionId { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }

            public string PhoneNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }

            public List<Stock> Stocks { get; set; }
        }

        public class Stock
        {
            public int StockId { get; set; }
            public int Qty { get; set; }

        }

        public async Task<bool> Do(Request request)
        {

            var order = new Order
            {
                OrderRef = CreatOrderRef(),
                CustomerPaymentRef = request.StripeRef,

                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                City = request.City,
                Address1 = request.Address1,
                Address2 = request.Address2,

                OrderStocks = request.Stocks.Select(s => new OrderStock
                {
                    StockId = s.StockId,
                    Qty = s.Qty
                }).ToList()

            };
           var success =  await _orderManager.CreatOrder(order) > 0;
            if (success)
            {
                await _stockManager.RemoveStockFromHold(request.SessionId);
                return true;
            }
            return false;

        }


        public string CreatOrderRef()
        {
            var chars = "JHFGJK86789755liopjFfgfgGJKhf54hgh19ss7j4nz7J38CM8kGHJJjHHJFUKFLKkjhfFkfGHKfdKDkGDktKgfjHjGlPlcPjGV4546F35";
            var result = new char[12];
            var random = new Random();

            do
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = chars[random.Next(chars.Length)];
                }
            } while (_orderManager.OrderRefIsExist(new string(result)));//check if orderRef is alrady exist


            return new string(result);
        }
    }
}
