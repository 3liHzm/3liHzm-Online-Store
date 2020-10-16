using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{

    [Route("[controller]/[action]")]
    public class CartController : Controller
    {

        [HttpPost("{id}")]
        public async Task<IActionResult>AddOne(
            int id,
            [FromServices]AddToCart addToCart)
        {
            var request = new AddToCart.Request
            {
                StockId = id,
                Qty = 1
            };
            var success = await addToCart.Do(request);
            if (success)
            {
                return Ok("Item added to cart");

            }
            return BadRequest("Failed to add to cart");
        }


     
        [HttpPost("{id}/{qty}")]
        public async Task<IActionResult>Remove(
            int id,
            int qty,
            [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = id,
                Qty = qty
            };
            var success = await removeFromCart.Do(request);
            if (success)
            {
                return Ok("Item remove from cart");

            }
            return BadRequest("Failed to remove from cart");
        }   
        
          [HttpGet]
         public IActionResult GetCartComponent([FromServices]GetCart getCart)
        {
            var total = getCart.Do().Sum(s => s.TheValue * s.Qty);

            return PartialView("Components/Cart/Small", $"${total}");
        }

        [HttpGet]
        public IActionResult GetCartMain([FromServices]GetCart getCart)
        {
            var cart = getCart.Do();

            return PartialView("_CartPartial", cart);
        }

    }
}
