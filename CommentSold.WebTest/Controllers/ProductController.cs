using System.Threading.Tasks;
using AutoMapper;
using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;
using CommentSold.WebTest.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommentSold.WebTest.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private ILogger<ProductController> _logger;
        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public ProductController(IProductRepository productRepository,
            ILogger<ProductController> logger, UserManager<ApplicationIdentityUser> userManager)
        {
            _logger = logger;
            _productRepository = productRepository;
            _userManager = userManager;
        }

        [HttpGet()]
        public async Task<IActionResult> GetProductsForUser(GetProductParameters productParameters)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var productsForUserFromRepo = _productRepository.GetProductsForUser(user.Id, productParameters);

            var productsForUser = Mapper.Map<PagedList<ProductDto>>(productsForUserFromRepo);

            return View(productsForUser);
        }

        [HttpGet()]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var productForUserFromRepo = _productRepository.GetProductForUser(user.Id, id);

            var productForUser = Mapper.Map<ProductDto>(productForUserFromRepo);

            return View(productForUser);
        }

    }
}
