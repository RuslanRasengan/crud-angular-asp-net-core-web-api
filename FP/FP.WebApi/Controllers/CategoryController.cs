using FP.DA;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FP.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FP.Interfaces.Category;
using FP.ViewModels.Category;

namespace FP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;

        private readonly ApplicationDbContext context;

        private readonly ICategoryService _categoryService;

        public CategoryController(
            ApplicationDbContext context,
            UserManager<AppUser> userManager,
            ICategoryService categoryService)
        {
            this.context = context;
            this.userManager = userManager;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategoriesAsync()
        {
            var userName = this.User.Identity.Name;

            var user = await userManager.FindByNameAsync(userName);

            return Ok(await _categoryService.GetCategoriesAsync(user));
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync([FromBody] CreateCategoryRequestViewModel model)
        {
            var userName = this.User.Identity.Name;

            var user = await userManager.FindByNameAsync(userName);

            try
            {
                if (ModelState.IsValid)
                {
                    await _categoryService.AddCategoryAsync(model, user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var categoryResponse = new CreateCategoryResponseViewModel
            {
                Status = "Success",
                Message = "Category created successfully!"
            };

            return Ok(categoryResponse);
        }
    }
}