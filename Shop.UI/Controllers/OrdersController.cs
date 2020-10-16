using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.OrdersAdmin;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy ="Manager")]
    public class OrdersController: Controller
    {


        [HttpGet("")]
        public IActionResult GetOrders(int status,
            [FromServices] GetOrders getOrders) =>
            Ok(getOrders.Do(status));

        [HttpGet("{id}")]
        public IActionResult GetOrder(int Id,
            [FromServices] GetOrder getOrder) =>
            Ok(getOrder.Do(Id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id,
            [FromServices] UpdateOrder updateOrder)
        {
            if (await updateOrder.Do(id) > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
           
        }
            

    }
}
