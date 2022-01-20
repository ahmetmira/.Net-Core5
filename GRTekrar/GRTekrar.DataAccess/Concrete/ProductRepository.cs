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
    public class ProductRepository : Repository<Product> , IProductRepository 
    {
        private GRTekrarDbContext context;
        public ProductRepository(GRTekrarDbContext context):base(context)
        {
            this.context = context;
        }

        public async Task<List<Product>> GetAllProduct(int pageNumber, int pageSize)
        {
            //.Skip((query.PageNumber - 1) * query.PageSize)
            //     .Take(query.PageSize).ToList();

            return await  context.Products.Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize).ToListAsync();
        }

        //public async Task<IEnumerable<Product>> GetProductsWithCategory(Category category)
        //{
        //    return (IEnumerable<Product>)await context.Categories.Include(a => a.Id == category.Id).ToListAsync();
        //}
    }
}
