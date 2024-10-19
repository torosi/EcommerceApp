using EcommerceApp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// dotnet ef migrations add Initial  --startup-project /Users/thomassimons/Documents/GitHub/EcommerceApp/EcommerceApp.MVC/EcommerceApp.MVC.csproj
// dotnet ef database update  --startup-project /Users/thomassimons/Documents/GitHub/EcommerceApp/EcommerceApp.MVC/EcommerceApp.MVC.csproj

// dotnet ef database update --startup-project C:\Users\thoma\source\repos\EcommerceApp\EcommerceApp.jMVC\EcommerceApp.MVC.cspro

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

        public DbSet<Product> Products { get; set; } // context.Products.Include(p => p.Image).ToList(); -- this is how you need to include images
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<Variation> Variations { get; set; }
        public DbSet<VariationValue> VariationValues { get; set; }
        public DbSet<VariationType> VariationTypes { get; set; }
        public DbSet<VariationAttribute> VariationAttributes { get; set; }



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

            builder.Entity<Product>()
                .HasOne(p => p.ProductType);

            // we want to make sure that the vartiationattibute class is made up of unique combinations
            builder.Entity<VariationAttribute>()
                .HasIndex(va => new { va.ProductVariationId, va.VariationId })
                .IsUnique();

            base.OnModelCreating(builder);
        }
    }
}
