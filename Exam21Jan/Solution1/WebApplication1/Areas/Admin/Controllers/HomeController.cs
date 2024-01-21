using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.Admin.ViewModels.ItemVMs;
using WebApplication1.Context;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
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
    }
}
