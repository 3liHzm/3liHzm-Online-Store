using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.UI.ViewModels.Admin;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
   
        }

        [HttpPost("")]
        public async Task<IActionResult> CreatUser([FromBody] CreatUserViewModel vm)
        {
            var managerUser = new IdentityUser()
            {
                UserName = vm.Username,

            };
               
         var ss =  await _userManager.CreateAsync(managerUser, vm.Password);
 
            if (!ss.Succeeded)
            {       
                return BadRequest("cant add user");
            }

            var managerClaim = new Claim("Role", "Manager");

            await _userManager.AddClaimAsync(managerUser, managerClaim);
            return Ok("User Created");
        }



        



    }
}
