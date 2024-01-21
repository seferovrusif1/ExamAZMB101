using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.Admin.ViewModels.ItemVMs;
using WebApplication1.Context;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    
    public class HomeController : Controller
    {
        Exam21JanDBContext _db { get; }

        public HomeController(Exam21JanDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var data = _db.Items.Select(c => new ListItemVM
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ImagePath = c.ImagePath,
                Category = c.Category.Name,
                IsDeleted = c.IsDeleted,
            });
            return View(data);
        }
        public IActionResult Create()
        {
            ViewBag.Category = _db.Categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ItemCreateVM vm)
        {
            if (vm.ImagePath != null)
            {
                if (!await vm.ImagePath.IsValidSize())
                {
                    ModelState.AddModelError("ImagePath", "File is bigger than 5mb");

                    ViewBag.Category = _db.Categories;
                    return View(vm);
                }
                if (!await vm.ImagePath.IsValidType())
                {
                    ModelState.AddModelError("ImagePath", "File is not image");
                    ViewBag.Category = _db.Categories;
                    return View(vm);
                }
            }
   
            if (!ModelState.IsValid)
            {
                ViewBag.Category = _db.Categories;
                return View();
            }
            await _db.Items.AddAsync(new Item
            {
                Title = vm.Title,
                Description = vm.Description,
                ImagePath =await  vm.ImagePath.ImageSaveAsync(PathConstants.ImageFolder),
                CategoryId = vm.Category
            });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index),"Home");
        }
        public IActionResult Update(int id)
        {
            var data = _db.Items.FindAsync(id).Result;

            ViewBag.Category = _db.Categories;
            return View(new ItemUpdateVM
            {
                Title=data.Title,
                Description=data.Description,
                LastImg=data.ImagePath
                
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,ItemUpdateVM vm)
        {
            if (vm.ImagePath!=null)
            {
            if (!await vm.ImagePath.IsValidSize())
            {
                ModelState.AddModelError("ImagePath", "File is bigger than 5mb");

                ViewBag.Category = _db.Categories;
                return View(vm);
            }
            if (!await vm.ImagePath.IsValidType())
            {
                ModelState.AddModelError("ImagePath", "File is not image");
                ViewBag.Category = _db.Categories;
                return View(vm);
            }
                
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Category = _db.Categories;
                return View(vm);
            }
            var data = _db.Items.FindAsync(id).Result;
            data.Title = vm.Title;
            data.Description = vm.Description;
            if (System.IO.File.Exists(Path.Combine(PathConstants.RoothPath, data.ImagePath)))
            {
                System.IO.File.Delete(Path.Combine(PathConstants.RoothPath, data.ImagePath));
            }
            data.ImagePath =await vm.ImagePath.ImageSaveAsync(PathConstants.ImageFolder);
            data.CategoryId=vm.Category;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Home");

        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = _db.Items.FindAsync(id).Result;
            if(System.IO.File.Exists(Path.Combine(PathConstants.RoothPath, data.ImagePath)))
            {
                System.IO.File.Delete(Path.Combine(PathConstants.RoothPath, data.ImagePath));
            }
            _db.Items.Remove(data);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index), "Home");
        }
        public async Task<IActionResult> SoftDelete(int id)
        {
            var data = _db.Items.FindAsync(id).Result;
            data.IsDeleted = true;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index), "Home");
        }
        public async Task<IActionResult> ReverseSoftDelete(int id)
        {
            var data = _db.Items.FindAsync(id).Result;
            data.IsDeleted = false;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index), "Home");
        }


    }
}
