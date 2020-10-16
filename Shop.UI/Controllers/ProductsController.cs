using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Products;
using Shop.Application.ProductsAdmin;
using Shop.Application.StockAdmin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{

    [Route("[controller]")]
    [Authorize(Policy = "Manager")]

    public class ProductsController : Controller
    {

        [HttpGet("")]
        public IActionResult GetProducts([FromServices] GetStocks getProducts) =>
            Ok(getProducts.Do());


        [HttpGet("{id}")]
        public IActionResult GetProduct(int id, [FromServices]Shop.Application.ProductsAdmin.GetProduct getProduct) => 
            Ok(getProduct.Do(id));


        [HttpPost("")]
        public async Task<IActionResult> CreatPorduct(CreateProduct.Request request, [FromServices] CreateProduct createProduct) =>  
            Ok(await createProduct.Do(request));   //didn;t pass the hossting



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id, [FromServices] DeleteProduct deleteProduct) => 
            Ok(await deleteProduct.Do(id));


        [HttpPut("")]
        public async Task<IActionResult> UpdateProduct(UpdateProduct.Request request,
            [FromServices] UpdateProduct updateProduct) =>
        Ok(await updateProduct.Do(request));  //didn;t pass the hossting


        [HttpPost("search/{term}")]
        public IActionResult Search(string term, [FromServices] SearchProducts searchProducts)
        {
            if(term == null)
            {
              return RedirectToPage("/Admin/Index");
            }
           return Ok(searchProducts.Do(term));
        }
            

    }
}
