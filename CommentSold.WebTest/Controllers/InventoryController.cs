using System.Threading.Tasks;
using AutoMapper;
using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;
using CommentSold.WebTest.Repositories;
using CommentSold.WebTest.Repositories.Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommentSold.WebTest.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly IInventoryDtoRepository _inventoryStore;
        private ILogger<InventoryController> _logger;
        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public InventoryController(IInventoryDtoRepository inventoryStore,
            ILogger<InventoryController> logger, UserManager<ApplicationIdentityUser> userManager)
        {
            _logger = logger;
            _inventoryStore = inventoryStore;
            _userManager = userManager;
        }

        [HttpGet()]
        public async Task<IActionResult> GetInventoryForUser(GetInventoryParameters inventoryParameters)
        {
            ViewData["MaximumQuantityFilter"] = inventoryParameters.MaximumQuantity;
            ViewData["SkuFilter"] = inventoryParameters.Sku;
            ViewData["ProductIdFilter"] = inventoryParameters.ProductId;

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var inventories = await _inventoryStore.GetInventoryForUserAsync(user.Id, inventoryParameters);
         
            return View(inventories);
        }

        [HttpGet()]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var inventory = await _inventoryStore.GetInventoryItemForUserAsync(user.Id, id);

            //TODO: explicitly handle id doesn't exist

            return View(inventory);
        }
    }
}
