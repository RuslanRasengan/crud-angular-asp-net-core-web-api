using FP.DA;
using FP.Entities;
using FP.Interfaces.Category;
using FP.ViewModels.Category;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FP.BL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
        }

        public async Task<List<GetCategoryResponseViewModel>> GetCategoriesAsync(AppUser user)
        {

            var Categories = await this.context.Categories
                .Where(a => a.UserId == null || a.UserId == user.Id)
                .Select(x => new GetCategoryResponseViewModel 
                { 
                    CategoryName = x.Name, 
                    CategoryType = x.IsIncome 
                }).ToListAsync();

            return Categories;
        }

        public async Task AddCategoryAsync(CreateCategoryRequestViewModel registrationDetails, AppUser user)
        {
            
            Category category = new Category()
            {
                Name = registrationDetails.Name,
                UserId = user.Id,
                IsIncome = registrationDetails.IsIncome
            };

            await context.AddAsync(category);

            await context.SaveChangesAsync();
        }
    }
}
