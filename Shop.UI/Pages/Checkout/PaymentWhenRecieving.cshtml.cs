using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Application.Orders;
using Shop.Domain.Infrastructure;
using GetOrderCart = Shop.Application.Cart.GetOrder;

namespace Shop.UI.Pages.Checkout
{
    public class PaymentWhenRecievingModel : PageModel
    {
        public IActionResult OnGet([FromServices] GetCustomerInfo getCustomerInfo)
        {
            var info = getCustomerInfo.Do();

            if (info == null)   //if he dosen't purches before
            {
                return RedirectToPage("/Checkout/CustomerInfo");
            }
            return Page();
        }


        public async Task<IActionResult> OnPost(
            [FromServices] GetOrderCart getOrder,
            [FromServices] CreateOrder createOrder,
            [FromServices] ISessionManager sessionManager)
        {

            var CartOrder = getOrder.Do();

            //creat order  //move it to CutomerInfo when u want to pay in 2stlsam

            var sessionId = HttpContext.Session.Id;


            await createOrder.Do(new CreateOrder.Request
            {

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
