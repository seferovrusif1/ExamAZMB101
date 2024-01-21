using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Context;
using WebApplication1.ViewModels.ItemVMs;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        Exam21JanDBContext _db { get; }

        public HomeController(Exam21JanDBContext db)
        {
            _db = db;
        }

        public async  Task<IActionResult> Index()
        {
            var data = _db.Items.Where(x => !x.IsDeleted).Take(3).Select(y => new HomeItemVM
            {
                Title = y.Title,
                Description = y.Description,
                ImagePath = y.ImagePath,
                Category = y.Category.Name
            }).ToList();
            return View(data);
        }

      
    }
}