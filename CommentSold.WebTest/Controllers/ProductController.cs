using System.Threading.Tasks;
using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Repositories;
using CommentSold.WebTest.Repositories.Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommentSold.WebTest.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private ILogger<ProductController> _logger;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly IProductDtoRepository _productStore;

        public ProductController(ILogger<ProductController> logger, UserManager<ApplicationIdentityUser> userManager,
            IProductDtoRepository productStore)
        {
            _logger = logger;
            _userManager = userManager;
            _productStore = productStore;
        }

        [HttpGet()]
        public async Task<IActionResult> GetProductsForUser(GetProductParameters productParameters)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var result = await _productStore.GetProductsForUserAsync(user.Id, productParameters);

            return View(result);
        }


        [HttpGet()]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var product = await _productStore.GetProductForUserAsync(user.Id, id);
            return View(product);
        }

    }
}
