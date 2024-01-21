using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.Admin.ViewModels;
using WebApplication1.Areas.Admin.ViewModels.CategoriesVMs;
using WebApplication1.Context;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        Exam21JanDBContext _db { get; }

        public SettingController(Exam21JanDBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View((Setting)_db.Setting.SingleOrDefault(x => x.Id == 1));
        }
        public IActionResult Update()
        {
            var data = _db.Setting.SingleOrDefault(x => x.Id == 1);

            return View(new SettingVM
            {
               
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(SettingVM vm)
        {
            if (vm.logo != null)
            {
                if (!await vm.logo.IsValidSize())
                {
                    ModelState.AddModelError("ImagePath", "File is bigger than 5mb");

                    return View(vm);
                }
                if (!await vm.logo.IsValidType())
                {
                    ModelState.AddModelError("ImagePath", "File is not image");
                    return View(vm);
                }

            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = _db.Setting.SingleOrDefault(x => x.Id == 1);
            if (System.IO.File.Exists(Path.Combine(PathConstants.RoothPath, data.logo)))
            {
                System.IO.File.Delete(Path.Combine(PathConstants.RoothPath, data.logo));
            }
            data.logo = await vm.logo.ImageSaveAsync(PathConstants.ImageFolder);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Setting");
        }
    }
}