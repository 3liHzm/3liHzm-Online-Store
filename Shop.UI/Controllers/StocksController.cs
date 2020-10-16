﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.StockAdmin;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{

    [Route("[controller]")]
    [Authorize(Policy = "Manager")]

    public class StocksController : Controller
    {



        [HttpGet("")]
        public IActionResult GetStock([FromServices] GetStocks getStocks) =>
            Ok(getStocks.Do());

        [HttpPost("")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStock.Request request,
            [FromServices] CreateStock createStock) =>
            Ok(await createStock.Do(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id, [FromServices] DeletStock deletStock) =>
            Ok(await deletStock.Do(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStock.Request request,
            [FromServices] UpdateStock updateStock) =>
            Ok(await updateStock.Do(request));

    }
}
