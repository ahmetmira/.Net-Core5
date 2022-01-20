using GRTrkrar.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRTekrar.Buisness.Abstract
{
    public interface ICategoryServices
    {
        Category GetCategoryById(int id);
        Task<Category> CreateCategory(Category category);
        Task<IEnumerable<Category>> GetAllCategory();
        void DeleteCategory(int id);
        Task<Category> UpdateCategory(Category category);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesWithProducts();
    }
}
