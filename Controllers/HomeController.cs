using Microsoft.AspNetCore.Mvc;

namespace GPUStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
