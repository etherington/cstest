using System;
using System.Threading.Tasks;
using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Repositories;
using CommentSold.WebTest.Repositories.Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vereyon.Web;

namespace CommentSold.WebTest.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class ProductsController : Controller
    {
        private ILogger<ProductsController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly ICachingProductRepository _cachingProductStore;
        private readonly IFlashMessage _flashMessage;

        public ProductsController(ILogger<ProductsController> logger, UserManager<ApplicationIdentityUser> userManager,
            ICachingProductRepository cachingProductStore, IProductRepository productRepository, IFlashMessage flashMessage)
        {
            _logger = logger;
            _userManager = userManager;
            _cachingProductStore = cachingProductStore;
            _productRepository = productRepository;
            _flashMessage = flashMessage;
        }

        [HttpGet()]
        [Route("products")]
        public async Task<IActionResult> GetProductsForUser(GetProductParameters productParameters)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var result = await _cachingProductStore.GetProductsForUserAsync(user.Id, productParameters);
            return View(result);
        }


        [HttpGet()]
        [Route("products/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var product = await _cachingProductStore.GetProductForUserAsync(user.Id, id);
            if (product == null)
            {
                return Unauthorized();
            }

            var productForList = AutoMapper.Mapper.Map<ProductForListDto>(product); 

            return View(productForList);
        }


        [HttpGet()]
        [Route("products/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("products/create")]
        public async Task<IActionResult> Create([FromForm] ProductForCreateDto product) 
        {
            if (product == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                try
                {
                    var productEntity = new Product(product.ProductName, product.Description, product.Style,
                        product.Brand,
                        product.Url, product.ProductType, product.ShippingPriceCents, product.Note, user);

                    productEntity = await _productRepository.AddProductAsync(productEntity);
                    var success = await _productRepository.SaveChanges();

                    await _cachingProductStore.InvalidateCache(productEntity.Id, user.Id);

                    _flashMessage.Info($"Successfully saved product: {productEntity.ProductName}");
                    return RedirectToAction(nameof(GetProductsForUser));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes.");
                }
            }
            return View();
        }

        [HttpGet()]
        [Route("products/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var product = await _cachingProductStore.GetProductForUserAsync(user.Id, id);
            
            if (product == null)
                return BadRequest();

            var productForEdit = AutoMapper.Mapper.Map<ProductForEditDto>(product); 

            return View(productForEdit);
        }

        [HttpPost]
        [Route("products/edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ProductForEditDto product)
        {
            if (product == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
              
                var productFromRepo = await _productRepository.GetProductForUserAsync(user.Id, product.Id);

                if (productFromRepo == null)
                {
                    //Either productId does not belong to user, or productId doesn't exist, or product has been deleted before the form was submitted
                    _flashMessage.Info($"Product no longer exists: {product.Id}");
                     ViewData["updatedVersion"] = null;
                    return View();
                }

                var timeDifference = Math.Abs((productFromRepo.UpdatedAt - product.UpdatedAt).TotalMilliseconds);
                if (timeDifference > Constants.ConcurrencyMilliseconds)
                {
                    // Someone else has updated the product in the database
                    ViewData["updatedVersion"] = productFromRepo;
                    return View();
                }

                try
                {
                    await _productRepository.UpdateProductAsync(product);
                    await _productRepository.SaveChanges();

                    _flashMessage.Info($"Successfully updated product: {productFromRepo.ProductName}");

                    await _cachingProductStore.InvalidateCache(product.Id, user.Id);

                    return RedirectToAction(nameof(Details), new { id = productFromRepo.Id });
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes.");
                }
            }
            return View();
        }
    }
}
