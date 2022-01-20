using GRTekrar.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRTekrar.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private GRTekrarDbContext dbContext;
        private ProductRepository productRepository;
        private CategoryRepository categoryRepository;
        public UnitOfWork(GRTekrarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IProductRepository Product => productRepository = productRepository ?? new ProductRepository(dbContext);

        public ICategoryRepository Category => categoryRepository = categoryRepository ?? new CategoryRepository(dbContext);

        public async Task<int> CommitAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
