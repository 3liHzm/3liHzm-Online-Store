using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Products;

namespace Shop.UI.Pages
{
    public class CategoryPageModel : PageModel
    {
        public IEnumerable<GetProductsByCatagory.ProductViewModel> Products { get; set; }

        public void OnGet(int catagoryId, [FromServices] GetProductsByCatagory getProductsByCatagory)
        {
            Products = getProductsByCatagory.Do(catagoryId);

        }
    }
}
