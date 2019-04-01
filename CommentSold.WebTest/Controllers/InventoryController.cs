using System.Threading.Tasks;
using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Repositories.Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommentSold.WebTest.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class InventoryController : Controller
    {
        private readonly ICachingInventoryRepository _cachingInventoryStore;
        private ILogger<InventoryController> _logger;
        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public InventoryController(ICachingInventoryRepository cachingInventoryStore,
            ILogger<InventoryController> logger, UserManager<ApplicationIdentityUser> userManager)
        {
            _logger = logger;
            _cachingInventoryStore = cachingInventoryStore;
            _userManager = userManager;
        }

        [HttpGet()]
        [Route("inventory")]
        public async Task<IActionResult> GetInventoryForUser(GetInventoryParameters inventoryParameters)
        {
            ViewData["MaximumQuantityFilter"] = inventoryParameters.MaximumQuantity;
            ViewData["SkuFilter"] = inventoryParameters.Sku;
            ViewData["ProductIdFilter"] = inventoryParameters.ProductId;

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var inventories = await _cachingInventoryStore.GetInventoryForUserAsync(user.Id, inventoryParameters);
         
            return View(inventories);
        }

        [HttpGet()]
        [Route("inventory/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var inventory = await _cachingInventoryStore.GetInventoryItemForUserAsync(user.Id, id);

            if (inventory==null)
            {
                return Unauthorized();
            }

            return View(inventory);
        }
    }
}
