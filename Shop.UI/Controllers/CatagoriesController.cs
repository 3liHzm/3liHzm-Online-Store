using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.CatagoryAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class CatagoriesController : Controller
    {
        [HttpGet("")]
        public IActionResult GetCatagories([FromServices] GetCatagories getCatagories) =>
             Ok(getCatagories.Do());

        [HttpPost("")]
        public async Task<IActionResult> CreateCatagory([FromBody] CreateCatagory.Request request,
                    [FromServices] CreateCatagory createCatagory) =>
                    Ok(await createCatagory.Do(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatagory(int id, [FromServices] DeleteCatagory deleteCatagory) =>
         Ok(await deleteCatagory.Do(id));


        [HttpPut("")]
        public async Task<IActionResult> UpdatCatagory([FromBody] UpdateCatagory.Request request,
            [FromServices] UpdateCatagory updateCatagory) =>
            Ok(await updateCatagory.Do(request));

    }
}
