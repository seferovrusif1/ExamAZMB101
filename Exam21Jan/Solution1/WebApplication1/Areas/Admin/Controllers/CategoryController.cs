using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.Admin.ViewModels.CategoriesVMs;
using WebApplication1.Areas.Admin.ViewModels.ItemVMs;
using WebApplication1.Context;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        Exam21JanDBContext _db { get; }

        public CategoryController(Exam21JanDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var data = _db.Categories.Select(c => new CategoryListItem
            {
                Id = c.Id,
                Name = c.Name,
                IsDeleted = c.IsDeleted,
            });
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
    
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _db.Categories.AddAsync(new Category
            {
                Name = vm.Name,
            });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Category");
        }
        public IActionResult Update(int id)
        {
            var data = _db.Categories.FindAsync(id).Result;

            return View(new CategoryUpdateVM
            {
                Name = data.Name,
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, CategoryUpdateVM vm)
        {
       
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = _db.Categories.FindAsync(id).Result;
            data.Name = vm.Name;
         
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Category");

        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = _db.Categories.FindAsync(id).Result;
            _db.Categories.Remove(data);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index), "Category");
        }
        public async Task<IActionResult> SoftDelete(int id)
        {
            var data = _db.Categories.FindAsync(id).Result;
            data.IsDeleted = true;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index), "Category");
        }
        public async Task<IActionResult> ReverseSoftDelete(int id)
        {
            var data = _db.Categories.FindAsync(id).Result;
            data.IsDeleted = false;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index), "Category");
        }
    }
}
