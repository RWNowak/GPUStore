using GPUStoreMVC.Models.Data;
using GPUStoreMVC.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GPUStoreMVC.Controllers
{
    public class GPUController : Controller
    {
        private readonly IGPUService _gpuService;
        private readonly IFileService _fileService;
        public GPUController(IGPUService GPUService, IFileService FileService)
        {
            _gpuService = GPUService;
            _fileService = FileService;
        }
        public IActionResult Add()
        {
            var model = new GPU();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(GPU model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.GPUImage = imageName;
            }
            var result = _gpuService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Edit(GPU model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.GPUImage = imageName;
            }
            var result = _gpuService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(GPUList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }
        public IActionResult Delete(int id)
        {
            var result = _gpuService.Delete(id);
            return RedirectToAction(nameof(GPUList));
        }
    }
}
