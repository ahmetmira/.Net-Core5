using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GRTekrar.DataAccess.Abstract
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T t);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> ts);
        T UpdateAsync(T t);
        void RemoveAsync(int id);
        IEnumerable<T> Find(Expression<Func<T,bool>> predicate);
        Task<bool> CheckIfExist(string name);
    }
}
