using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// dotnet ef migrations add AddProductTypeVariationMappingsTable  --startup-project /Users/thomassimons/Documents/GitHub/EcommerceApp/EcommerceApp.MVC/EcommerceApp.MVC.csproj
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

        public DbSet<VariationValue> VariationValues { get; set; }
        public DbSet<VariationType> VariationTypes { get; set; }
        public DbSet<ProductTypeVariationMapping> ProductTypeVariationMappings { get; set; }
        public DbSet<ProductVariationOption> ProductVariationOptions { get; set; }
        public DbSet<Sku> Skus { get; set; }



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

            // // we want to make sure that the vartiationattibute class is made up of unique combinations
            // builder.Entity<VariationAttribute>()
            //     .HasIndex(va => new { va.ProductVariationId, va.VariationId })
            //     .IsUnique();

            // Define the primary key for the table as the Id
            builder.Entity<ProductVariationOption>()
                .HasKey(p => p.Id);

            // Create a unique constraint on the combination of Sku and VariationType
            builder.Entity<ProductVariationOption>()
                .HasIndex(p => new { p.SkuId, p.VariationTypeId }) // we dont want to allow the same sku to have the multiple of the same variation type (one size, one colour etc)
                .IsUnique(); // Ensure no repeats of VariationType for a given Sku

            builder.Entity<ProductTypeVariationMapping>()
                .HasKey(p => new { p.ProductTypeId, p.VariationTypeId });



            base.OnModelCreating(builder);
        }
    }
}
