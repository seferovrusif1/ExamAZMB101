using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Item:BaseModel
    {
        [Required,MaxLength(128)]
        public string Title { get; set; }
        [Required, MaxLength(256)]
        public string Description { get; set; }
        [Required, MaxLength(128)]
        public string ImagePath { get; set; }
        [Required]
        public int CategoryId {  get; set; }
        public Category Category { get; set; }
    }
}
