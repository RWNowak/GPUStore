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
        public IActionResult Index(string term ="", int currentpage = 1)
        {
            var gpus = _gpuService.List(term, true, currentpage);
            return View(gpus);
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult GPUDetail(int GPUID)
        {
            var gpu = _gpuService.GetById(GPUID);
            return View(gpu);
        }
    }
}
