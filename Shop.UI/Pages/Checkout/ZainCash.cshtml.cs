using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Application.Orders;
using Shop.Domain.Infrastructure;
using GetOrderCart = Shop.Application.Cart.GetOrder;

namespace Shop.UI.Pages.Checkout
{
    public class ZainCashModel : PageModel
    {

        //public string Token { get; set; }
        //private readonly IConfiguration config;

        //public ZainCashModel(IConfiguration config)
        //{
        //    this.config = config;
        //}
        //public void OnGet()
        //{

        //}

        //public void OnPost(int orderId, [FromServices] GetOrderCart getOrder,
        //    [FromServices] CreateOrder createOrder,
        //    [FromServices] ISessionManager sessionManager)
        //{

        //    var data = new Data
        //    {
        //        Amount = getOrder.Do().GetCharge(),
        //        ServiceType = "blah blah",
        //        Msisdn = "9647835077880",
        //        OrderId = orderId,
        //        RedirectUrl = "http://5000/index",
        //        Iat = DateTime.Now,
        //        Exp = DateTime.Now.AddDays(1)

        //    };
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //            {
        //                new Claim(ClaimTypes.Anonymous, data.ToString()),
        //            }),
        //        Expires = DateTime.Now.AddDays(1),
        //        SigningCredentials = creds
        //    };
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescriptor);

        //    Token = tokenHandler.WriteToken(token);


        //}

        //public class Data
        //{
        //    public decimal Amount { get; set; }
        //    public string ServiceType { get; set; }
        //    public string Msisdn { get; set; }
        //    public int OrderId { get; set; }
        //    public string RedirectUrl { get; set; }
        //    public DateTime Iat { get; set; }
        //    public DateTime Exp { get; set; }
        //}
    }
}
