using Generali.Entity.DTO.Users;
using Generali.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Generali.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> UserManager;
        private readonly SignInManager<AppUser> SignInManager;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(userLoginDto.Email);
                if (user != null)
                {
                    var result = await SignInManager.PasswordSignInAsync(user, userLoginDto.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home", new { Area = ""});
                    }
                    else
                    {
                        ModelState.AddModelError("", "Wrong Email or Password!");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong Email or Password!");
                    return View();
                }
            }
            else
            {
                return View();
            }
            
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new {Area = ""});
        }
    }
}
