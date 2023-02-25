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
            if (model.ImageFile != null)
            {
                var fileResult = this._fileService.SaveImage(model.ImageFile);
                if (fileResult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileResult.Item2;
                model.GPUImage = imageName;
            }
            var result = _gpuService.Edit(model);
            if (result)
            {
                TempData["msg"] = "Edited Successfully";
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
            return RedirectToAction(nameof(GPUList));
        }

        public IActionResult GPUList()
        {
            //var data = this._gpuService.List();
            //return View(data.GPUList);

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
