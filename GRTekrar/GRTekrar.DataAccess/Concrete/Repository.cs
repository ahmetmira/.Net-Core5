using GRTekrar.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GRTekrar.DataAccess.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private GRTekrarDbContext dbContext;
        private DbSet<T> ts1;

        public Repository(GRTekrarDbContext dbContext)
        {
            this.dbContext = dbContext;
            ts1 = dbContext.Set<T>();
        }
        public async Task<T> AddAsync(T t)
        {
            await dbContext.Set<T>().AddAsync(t);
            return t;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> ts)
        {
            await dbContext.Set<T>().AddRangeAsync(ts);
            return ts;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Where(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<bool> CheckIfExist(string name)
        {
           return await dbContext.Products.Where(x => x.Name.ToLower() == name.ToLower()).AnyAsync() ? true : false;
     
        }
        public T GetById(int id)
        {
            return ts1.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public void RemoveAsync(int id)
        {
            //RemoveAsync olmadığı için await kullanmadık
            var delete = ts1.Find(id);
            ts1.Remove(delete);
        }

        public T UpdateAsync(T t)
        {
            dbContext.Set<T>().Update(t);
            return t;
        }
    }
}
