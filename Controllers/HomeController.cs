using GPUStoreMVC.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace GPUStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGPUService _gpuService;

        public HomeController(IGPUService GPUService)
        {
            _gpuService= GPUService;
        }
        public IActionResult Index()
        {
            var gpus = _gpuService.List();
            return View(gpus);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
