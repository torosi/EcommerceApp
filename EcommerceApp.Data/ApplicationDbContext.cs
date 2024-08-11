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
        public DbSet<Product> Products{ get; set; } // context.Products.Include(p => p.Image).ToList(); -- this is how you need to imclude images

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

            builder.Entity<Product>()
                .HasOne(p => p.Image)
                .WithMany() // No navigation property in Image
                .HasForeignKey(p => p.ImageId);

            base.OnModelCreating(builder);
        }
    }
}
