using GRTekrar.Buisness.Abstract;
using GRTekrar.DataAccess.Abstract;
using GRTrkrar.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRTekrar.Buisness.Concrete
{
    public class CategoryServices : ICategoryServices
    {
        private IUnitOfWork unitOfWork;
        public CategoryServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Category> CreateCategory(Category category)
        {
            await unitOfWork.Category.AddAsync(category);
            await unitOfWork.CommitAsync();
            return category;
        }

        public void DeleteCategory(int id)
        {
            unitOfWork.Category.RemoveAsync(id);
            unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await unitOfWork.Category.GetAllAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithProducts()
        {
            return await unitOfWork.Category.GetCategoriesWithProducts();
        }

        public Category GetCategoryById(int id)
        {
            return unitOfWork.Category.GetById(id);
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await unitOfWork.Category.GetByIdAsync(id);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            unitOfWork.Category.UpdateAsync(category);
            await unitOfWork.CommitAsync();
            return category;
        }
    }
}
