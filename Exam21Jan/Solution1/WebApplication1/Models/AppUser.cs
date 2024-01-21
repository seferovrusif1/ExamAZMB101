using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AppUser:IdentityUser
    {
        [Required,MaxLength(64)]
        public string Fullname { get; set; }
    }
}
