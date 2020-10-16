using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;


namespace Shop.UI.Pages.Checkout
{
    public class CustomerInfoModel : PageModel
    {
        //private readonly IHostingEnvironment _env;

        //public CustomerInfoModel(IHostingEnvironment env)
        //{
        //    _env = env;

        //}

        [BindProperty]
        public AddCustomerInfo.Request CustomerInfo { get; set; }                    

        public IActionResult OnGet([FromServices] GetCustomerInfo getCustomerinfo)
        {
            //Get Cart
            var info = getCustomerinfo.Do();

            if(info == null)
            {       
                //if (_env.IsDevelopment())
                //{
                //    CustomerInfo = new AddCustomerInfo.Request
                //    {
                //        FirstName = "a",
                //        LastName = "a",
                //        Email = "defult@defult.defult",
                //        PhoneNumber = "00",
                //        City = "defult",
                //        Address1 = "defult",
                //        Address2 = "defult"
                //    };
                //}
                return Page();
            }
            else
            {
                return RedirectToPage("/Checkout/PaymentMethods");
            }

        }

        public IActionResult OnPost([FromServices] AddCustomerInfo addCustomerinfo)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            addCustomerinfo.Do(CustomerInfo);

            return RedirectToPage("PaymentMethods");
        }
    }
}
