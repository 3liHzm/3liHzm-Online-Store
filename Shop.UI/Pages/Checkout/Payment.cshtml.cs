using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Shop.Application.Cart;
using Shop.Application.Orders;
using Shop.Domain.Infrastructure;
using Stripe;
using GetOrderCart = Shop.Application.Cart.GetOrder;

namespace Shop.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    { 
        public string PublicKey { get; }



        public PaymentModel(IConfiguration config)
        {
            PublicKey = config["Stripe:PublicKey"].ToString();
        }

        public IActionResult OnGet([FromServices] GetCustomerInfo getCustomerInfo)
        {
            var info = getCustomerInfo.Do();

            if (info == null)   //if he dosen't purches before
            {
                return RedirectToPage("/Checkout/CustomerInfo");
            }
            return Page();
        }


        public async Task<IActionResult> OnPost(string stripeEmail, string stripeToken,
            [FromServices] GetOrderCart getOrder,
            [FromServices] CreateOrder createOrder,
            [FromServices] ISessionManager sessionManager)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var CartOrder = getOrder.Do();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken ,
                  
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = CartOrder.GetCharge(),
                Description = "Purchases",
                Currency = "usd",
                Customer = customer.Id
            });
            //creat order  //move it to CutomerInfo when u want to pay in 2stlsam

            var sessionId = HttpContext.Session.Id;

            
            await createOrder.Do(new CreateOrder.Request
            {
                StripeRef = charge.Id,

                SessionId = sessionId,

                FirstName = CartOrder.CustomerInfo.FirstName,
                LastName = CartOrder.CustomerInfo.LastName,
                Email = CartOrder.CustomerInfo.Email,
                PhoneNumber = CartOrder.CustomerInfo.PhoneNumber,
                City = CartOrder.CustomerInfo.City,
                Address1 = CartOrder.CustomerInfo.Address1,
                Address2 = CartOrder.CustomerInfo.Address2,

                Stocks = CartOrder.Products.Select(s => new CreateOrder.Stock
                {
                    StockId = s.StockId,
                    Qty = s.Qty
                }).ToList()

            });
            // HttpContext.Session.Remove();//clear the cart after sucses payment
            sessionManager.CleanCart();
            //TODO Send sucsess msg
            return RedirectToPage("/index");
        }
                                                                
    }
}
