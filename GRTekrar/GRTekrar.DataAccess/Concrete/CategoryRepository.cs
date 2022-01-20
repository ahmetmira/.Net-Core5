using GRTekrar.DataAccess.Abstract;
using GRTrkrar.Entities.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRTekrar.DataAccess.Concrete
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private GRTekrarDbContext Context;
        public CategoryRepository(GRTekrarDbContext context):base(context)
        {
            this.Context = context;
        }

        public async Task<List<Category>> GetAllCategories(string productName ,int pageSize, int pageNumber)
        {
            pageNumber = 1;
            pageSize = 20;
            var newList = new List<Category>();
            var productNames = new List<string>();
           var list= await Context.Categories.Include(x => x.Products).ToListAsync();
            if (productName != null)
            {
                foreach(var category in list)
                {
                    foreach(var product in category.Products)
                    {
                        if (product.Name == productName)
                        {
                            if (!productNames.Contains(product.Name))
                            {
                                newList.Add(category);
                                productNames.Add(product.Name);
                            }

                        }

                    }
                }
            }
            else
            {
                newList = list;
            }

            //if (categoryName != null)
            //{
            //    newList=newList.Where()
            //}
            var x = newList.Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize).ToList();
            return await Task.FromResult(x);
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithProducts()
        {
            return await Context.Categories.Include(x=>x.Products).ToListAsync();
        }
    }
}
