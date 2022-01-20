using GRTrkrar.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRTekrar.DataAccess.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesWithProducts();
        Task<List<Category>> GetAllCategories(string product,int pageSize, int pageNumber );
    }
}
