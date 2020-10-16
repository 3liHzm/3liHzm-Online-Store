using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.CatagoryAdmin;
using Shop.Application.Products;
using Shop.Domain.Models;



namespace Shop.UI.Pages
{

    public class IndexModel : PageModel
    {
        [BindProperty]
        public IEnumerable<CategoriesViewModel> Catagories { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }

        public void OnGet([FromServices] GetProducts getProducts,
            [FromServices] GetCatagories getCatagories)
        {
            Products = getProducts.Do();
            Catagories = getCatagories.Do();
    

        }

       public void OnPost(string term,
              [FromServices] SearchForProducts searchForProducts )
        {

            Products = searchForProducts.Do(term);
        }
    }
}
