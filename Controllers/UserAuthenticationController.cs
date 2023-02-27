using GPUStoreMVC.Models.Other;
using GPUStoreMVC.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace GPUStoreMVC.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthentication authService;

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
                TempData["msg"] = "Logged in successfully";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Wrong username or password";
                return RedirectToAction(nameof(Login));
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Registration model)
        {
            if (!ModelState.IsValid) { return View(model); }
            model.Role = "user";
            var result = await this.authService.RegisterAsync(model);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Register));
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            TempData["msg"] = "Logged out successfully";
            return RedirectToAction("Index", "Home");
        }

    }
}