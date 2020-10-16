//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Shop.UI.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly SignInManager<IdentityUser> _signInManager;

//        public AccountController(SignInManager<IdentityUser> signInManager)
//        {
//            _signInManager = signInManager;
//        }

//        [HttpGet]
//        public async Task<IActionResult> Logout()
//        {
//            await _signInManager.SignOutAsync();
//            return RedirectToPage("/Index");
//        }
//    }
//}


//DELETE IT THEN 