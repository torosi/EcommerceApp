using EcommerceApp.Data.Entities;
using EcommerceApp.Data.Entities.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// dotnet ef migrations add ChangingShoppingCartTable  --startup-project /Users/thomassimons/Documents/GitHub/EcommerceApp/EcommerceApp.MVC/EcommerceApp.MVC.csproj
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

        public DbSet<ApplicationUserEntity> ApplicationUsers { get; set; }

        public DbSet<ProductEntity> Products { get; set; } // context.Products.Include(p => p.Image).ToList(); -- this is how you need to include images
        public DbSet<ProductTypeEntity> ProductTypes { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ShoppingCartEntity> ShoppingCarts { get; set; }

        public DbSet<VariationValueEntity> VariationValues { get; set; }
        public DbSet<VariationTypeEntity> VariationTypes { get; set; }
        public DbSet<ProductTypeVariationMappingEntity> ProductTypeVariationMappings { get; set; }
        public DbSet<ProductVariationOptionEntity> ProductVariationOptions { get; set; }
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
            builder.Entity<ProductEntity>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            builder.Entity<ProductEntity>()
                .HasOne(p => p.ProductType);


            // // we want to make sure that the vartiationattibute class is made up of unique combinations
            // builder.Entity<VariationAttribute>()
            //     .HasIndex(va => new { va.ProductVariationId, va.VariationId })
            //     .IsUnique();

            // Define the primary key for the table as the Id
            builder.Entity<ProductVariationOptionEntity>()
                .HasKey(p => p.Id);

            // Create a unique constraint on the combination of Sku and VariationType
            builder.Entity<ProductVariationOptionEntity>()
                .HasIndex(p => new { p.SkuId, p.VariationTypeId }) // we dont want to allow the same sku to have the multiple of the same variation type (one size, one colour etc)
                .IsUnique(); // Ensure no repeats of VariationType for a given Sku

            builder.Entity<ProductTypeVariationMappingEntity>()
                .HasKey(p => new { p.ProductTypeId, p.VariationTypeId });

            builder.Entity<Sku>()
                .HasMany(s => s.ProductVariationOptions)
                .WithOne(pvo => pvo.Sku)
                .HasForeignKey(pvo => pvo.SkuId)
                .OnDelete(DeleteBehavior.Cascade); // IMPORTANT - how will this impact deleting? other tables already have this in sqlserver?

            // Apply Unique Constraint on SkuString
            builder.Entity<Sku>()
                .HasIndex(s => s.SkuString)
                .IsUnique();

            base.OnModelCreating(builder);
        }
    }
}
