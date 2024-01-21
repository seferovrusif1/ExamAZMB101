using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Category:BaseModel
    {
        [Required,MaxLength(64)]
        public string Name { get; set; }
        public IEnumerable<Item> Items { get; set; }

    }
}
