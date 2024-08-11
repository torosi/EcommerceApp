using EcommerceApp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// dotnet ef migrations add Initial  --startup-project /Users/thomassimons/Documents/GitHub/EcommerceApp/EcommerceApp.MVC/EcommerceApp.MVC.csproj
// dotnet ef database update --startup-project C:\Users\thoma\source\repos\EcommerceApp\EcommerceApp.MVC\EcommerceApp.MVC.csproj

namespace EcommerceApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Products{ get; set; } // context.Products.Include(p => p.Image).ToList(); -- this is how you need to imclude images
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Image> Images { get; set; }

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

            // product images
            builder.Entity<ProductImage>()
                .HasKey(pi => new { pi.ProductId, pi.ImageId});

            builder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId);
            
            builder.Entity<ProductImage>()
                .HasOne(pi => pi.Image)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ImageId);

            base.OnModelCreating(builder);
        }
    }
}
