using EcommerceApp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection.Emit;

// dotnet ef migrations add Initial  --startup-project /Users/thomassimons/Documents/GitHub/EcommerceApp/EcommerceApp.MVC/EcommerceApp.MVC.csproj
// dotnet ef database update --startup-project C:\Users\thoma\source\repos\EcommerceApp\EcommerceApp.MVC\EcommerceApp.MVC.csproj

//# Add migration
//Add-Migration MigrationName

//# Update database
//Update-Database

namespace EcommerceApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Products{ get; set; } // context.Products.Include(p => p.Image).ToList(); -- this is how you need to imclude images
        //public DbSet<ProductVariation> ProductVariations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Seed user roles
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

            // Configure Product-Category relationship
            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            base.OnModelCreating(builder);
        }
    }
}
