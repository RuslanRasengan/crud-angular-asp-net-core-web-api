using FP.Entities;
using FP.ViewModels.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FP.Interfaces.Category
{
    public interface ICategoryService
    {
        Task<List<GetCategoryResponseViewModel>> GetCategoriesAsync(AppUser user);
        Task AddCategoryAsync(CreateCategoryRequestViewModel registrationDetails, AppUser user);

    }
}
