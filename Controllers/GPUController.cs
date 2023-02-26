using GPUStoreMVC.Models.Data;
using GPUStoreMVC.Models.Other;
using GPUStoreMVC.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GPUStoreMVC.Controllers
{
    public class GPUController : Controller
    {
        private readonly IGPUService _gpuService;
        private readonly IFileService _fileService;
        private readonly DatabaseContext _dbcontext;
        public GPUController(IGPUService GPUService, IFileService FileService, DatabaseContext dbcontext)
        {
            _gpuService = GPUService;
            _fileService = FileService;
            _dbcontext = dbcontext;
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
                var fileResult = this._fileService.SaveImage(model.ImageFile);
                if (fileResult.Item1 == 0)
                {
                    TempData["msg"] = "A problem occured while uploading an image";
                    return View(model);
                }
                var imageName = fileResult.Item2;
                model.GPUImage = imageName;
            }
            var result = _gpuService.Add(model);
            if (result)
            {
                TempData["msg"] = "GPU added successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }
        public IActionResult Edit(int GPUID)
        {
            var model = _gpuService.GetById(GPUID);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int GPUID, GPU model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var gpuToUpdate = _gpuService.GetById(GPUID);
            if (gpuToUpdate == null)
            {
                return NotFound();
            }

            gpuToUpdate.Name = model.Name;
            gpuToUpdate.Price = model.Price;
            gpuToUpdate.ReleaseDate = model.ReleaseDate;
            gpuToUpdate.Memory = model.Memory;
            gpuToUpdate.Bus = model.Bus;

            if (model.ImageFile != null)
            {
                var fileResult = this._fileService.SaveImage(model.ImageFile);
                if (fileResult.Item1 == 0)
                {
                    TempData["msg"] = "File could not be saved";
                    return View(model);
                }
                var imageName = fileResult.Item2;
                gpuToUpdate.GPUImage = imageName;
            }
            var result = _gpuService.Edit(gpuToUpdate);
            if (result)
            {
                TempData["msg"] = "GPU edited successfully";
                return RedirectToAction(nameof(GPUList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }
        public IActionResult Delete(int GPUID)
        {
            var result = _gpuService.Delete(GPUID);
            if (result)
            {
                TempData["msg"] = "GPU deleted successfully";
                return RedirectToAction(nameof(GPUList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return RedirectToAction(nameof(GPUList));
            }
            
        }
        public IActionResult GPUList()
        {
            var gpuList = _dbcontext.GPUs.AsQueryable();
            var viewModel = new GPUListVM
            {
                GPUList = gpuList,
                PageSize = 10,
                CurrentPage = 1,
                TotalPages = (int)Math.Ceiling(gpuList.Count() / (double)10)
            };
            return View(viewModel);
        }
    }
}
