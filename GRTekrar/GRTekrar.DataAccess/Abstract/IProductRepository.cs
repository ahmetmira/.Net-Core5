using GRTrkrar.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRTekrar.DataAccess.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        //Task<IEnumerable<Product>> GetAllProduct(GetAllProductsQuery);
        Task<List<Product>> GetAllProduct(int pageNumber , int pageSize);

    }
}
