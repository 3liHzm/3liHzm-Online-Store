using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Products;
using Shop.Application.Cart;

namespace Shop.UI.Pages
{
    public class ProductModel : PageModel
    {

        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }
  
        public GetProduct.ProductViewModel Product { get; set; }

        public async Task<IActionResult> OnGet(string name, [FromServices] GetProduct getProduct)
        {
            Product =await getProduct.Do(name.Replace("--", " "));
            if (Product == null)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }

           
        }
        public async Task<IActionResult> OnPost([FromServices] AddToCart addToCart)
        {
          var stockAdded = await addToCart.Do(CartViewModel);

            
            if (stockAdded)
            {
                return RedirectToPage("Cart");
            }

            else
            {   
                //todo add warning msg
                return RedirectToPage("Product");
            }

        }
    }
}
