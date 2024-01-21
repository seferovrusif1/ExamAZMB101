using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class Exam21JanDBContext : IdentityDbContext<AppUser>
    {
        public Exam21JanDBContext(DbContextOptions options) : base(options){}

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Setting> Setting { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>().HasData(
                new Setting
                {
                    Id = 1,
                    logo= "assets/imgs/placeholder.svg"
                }
            );
            base.OnModelCreating(modelBuilder);
        }

    }
}
