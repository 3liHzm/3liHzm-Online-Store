using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Orders;

namespace Shop.UI.Pages
{
    public class OrderModel : PageModel
    {

        public GetOrder.Response Order { get; set; }

        public void OnGet(string refrence, [FromServices] GetOrder getOrder)
        {
            Order = getOrder.Do(refrence);
        }
    }
}
