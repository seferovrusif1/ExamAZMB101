using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class Exam21JanDBContext : IdentityDbContext<AppUser>
    {
        public Exam21JanDBContext(DbContextOptions options) : base(options){}

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
