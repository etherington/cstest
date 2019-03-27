﻿using System.Threading.Tasks;
using AutoMapper;
using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommentSold.WebTest.Controllers
{
    [Authorize]
    [Route("inventory")]
    public class InventoryController : Controller
    {
        private readonly IInventoryRepository _inventoryRepository;
        private ILogger<InventoryController> _logger;
        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public InventoryController(IInventoryRepository inventoryRepository,
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

            var inventoryForUserFromRepo = _inventoryRepository.GetInventoryForUser(user.Id, inventoryParameters);

            var inventoryForUser = Mapper.Map<PagedList<InventoryDto>>(inventoryForUserFromRepo);
           
            return View(inventoryForUser);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> Details(int Id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var inventoryItemForUserFromRepo = _inventoryRepository.GetInventoryItemForUser(user.Id, Id);

            var inventoryItemForUser = Mapper.Map<InventoryDto>(inventoryItemForUserFromRepo);

            return View(inventoryItemForUser);
        }
    }
}