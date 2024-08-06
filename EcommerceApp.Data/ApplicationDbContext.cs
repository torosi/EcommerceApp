using EcommerceApp.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "afkjhsadfoiusdfpoqweqlkasd",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "ukhdflkahdfkajasdfklasd",
                    Name = "User",
                    NormalizedName = "USER"
                }
            );

            base.OnModelCreating(builder);
        }
    }
}
