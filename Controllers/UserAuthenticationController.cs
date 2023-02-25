using GPUStoreMVC.Models.Other;
using GPUStoreMVC.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace GPUStoreMVC.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthentication authService;

        public UserAuthenticationController(IUserAuthentication authService)
        {
            this.authService = authService;
        }
        //Create a user with admin priviliges, and then comment out
        //public async Task<IActionResult> Register()
        //{
        //    var model = new Registration
        //    {
        //        Email = "admin@gmail.com",
        //        Username = "Admin",
        //        Name = "Admin",
        //        Password = "Admin@123",
        //        PasswordConfirm = "Admin@123",
        //        Role = "Admin",
        //    };
        //    var result = await authService.RegisterAsync(model);
        //    return Ok(result.Message);

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await authService.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = "Wrong username or password";
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}