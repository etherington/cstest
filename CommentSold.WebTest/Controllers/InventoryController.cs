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
    public class InventoryController : Controller
    {
        private readonly IAsyncInventoryRepository _inventoryRepository;
        private ILogger<InventoryController> _logger;
        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public InventoryController(IAsyncInventoryRepository inventoryRepository,
            ILogger<InventoryController> logger, UserManager<ApplicationIdentityUser> userManager)
        {
            _logger = logger;
            _inventoryRepository = inventoryRepository;
            _userManager = userManager;
        }

        [HttpGet()]
        public async Task<IActionResult> GetInventoryForUser(GetInventoryParameters inventoryParameters)
        {
            ViewData["MaximumQuantityFilter"] = inventoryParameters.MaximumQuantity;
            ViewData["SkuFilter"] = inventoryParameters.Sku;
            ViewData["ProductIdFilter"] = inventoryParameters.ProductId;

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var inventoryForUserFromRepo = await _inventoryRepository.GetInventoryForUserAsync(user.Id, inventoryParameters);

            var inventoryForUser = Mapper.Map<PagedList<InventoryDto>>(inventoryForUserFromRepo);
           
            return View(inventoryForUser);
        }

        [HttpGet()]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var inventoryItemForUserFromRepo = await _inventoryRepository.GetInventoryItemForUserAsync(user.Id, id);

            var inventoryItemForUser = Mapper.Map<InventoryDto>(inventoryItemForUserFromRepo);

            return View(inventoryItemForUser);
        }
    }
}
