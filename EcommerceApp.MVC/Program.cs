using EcommerceApp.Data;
using EcommerceApp.Data.Repositories;
using EcommerceApp.Data.Repositories.Contracts;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.Domain.Services.Implementations;
using EcommerceApp.MVC.Automapper;
using EcommerceApp.MVC.Helpers;
using EcommerceApp.MVC.Helpers.Interfaces;
using EcommerceApp.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

/*builder.Services.AddDatabaseDeveloperPageExceptionFilter();*/

//Singleton: This creates only one instance of a class during the application's lifecycle.
//Transient: Every time you request a transient class, a new instance is created.
//Scoped: Scoped instances are created once per client request.

builder.Services.AddLogging();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();

builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();

builder.Services.AddScoped<IVariationTypeRepository, VariationTypeRepository>();
builder.Services.AddScoped<IVariationTypeService, VariationTypeService>();

builder.Services.AddScoped<ISkuRepository, SkuRepository>();
builder.Services.AddScoped<ISkuService, SkuService>();

builder.Services.AddScoped<IProductVariationOptionRepository, ProductVariationOptionRepository>();



// The ImageHelper class is registered as a singleton service using AddSingleton<ImageHelper>().
// This means that a single instance of ImageHelper will be created and shared throughout the application.
builder.Services.AddSingleton<ImageHelper>();

builder.Services.AddScoped<IUserHelper, UserHelper>();


builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // has to be this way around
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
