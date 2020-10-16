using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.UI.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
           var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Pasword, false, false);

            if(result.Succeeded)
            {
                return RedirectToPage("/Admin/Index");

            }
            else
            {
                //TODO return warn msg
                return Page();
            }
        }

        public class LoginViewModel
        {
            public string UserName { get; set; }
            public string Pasword { get; set; }
        }
    }                               
}
