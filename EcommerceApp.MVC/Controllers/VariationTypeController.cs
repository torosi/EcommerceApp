using EcommerceApp.Domain.Models.Products;
using EcommerceApp.Domain.Services.Contracts;
using EcommerceApp.MVC.Models.Product;
using EcommerceApp.MVC.Models.ProductType;
using EcommerceApp.MVC.Models.VariationType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Common;

namespace EcommerceApp.MVC.Controllers
{
    public class VariationTypeController : Controller
    {
        private readonly IVariationTypeService _variationTypeService;
        private readonly IProductTypeService _productTypeService;

        public VariationTypeController(IVariationTypeService variationTypeService, IProductTypeService productTypeService)
        {
            _variationTypeService = variationTypeService;
            _productTypeService = productTypeService;
        }

        // GET: VariationTypeController
        public async Task<ActionResult> Index()
        {
            try 
            {
                var variationTypeModels = await _variationTypeService.GetAllAsync();
                var variationTypeViewModels = variationTypeModels.Select(x => new VariationTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Created = x.Created,
                    Updated = x.Updated
                });

                return View(variationTypeViewModels);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<ActionResult> Create()
        {
            try 
            {
                // var productTypeModels = await _productTypeService.GetAllAsync();
                // var productTypeViewModels = productTypeModels.Select(x => new ProductTypeViewModel()
                // {
                //     Id = x.Id,
                //     Name = x.Name,
                //     Description = x.Description,
                //     Created = x.Created,
                //     Updated = x.Updated
                // });

                // var createVariationTypeViewModel = new CreateVariationTypeViewModel()
                // {
                //     ProductTypes = productTypeViewModels
                // };

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateVariationTypeViewModel variationType)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    // map view model to dto
                    var variationTypeModel = new VariationTypeModel()
                    { 
                        Id = 0,
                        Name = variationType.Name,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    // save to db
                    await _variationTypeService.CreateVariationTypeAsync(variationTypeModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
