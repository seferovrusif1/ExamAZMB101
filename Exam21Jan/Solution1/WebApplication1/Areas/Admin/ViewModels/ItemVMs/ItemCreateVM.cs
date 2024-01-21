using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.ViewModels.ItemVMs
{
    public class ItemCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImagePath { get; set; }
        public int Category { get; set; }
    }
}
