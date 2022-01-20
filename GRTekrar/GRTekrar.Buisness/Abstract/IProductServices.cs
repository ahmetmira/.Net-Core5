using GRTrkrar.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRTekrar.Buisness.Abstract
{
    public interface IProductServices
    {
        Product GetProductById(int id);
        Task<Product> CreateProduct(Product product);
        Task<IEnumerable<Product>> GetAllProduct();
        void DeleteProduct(int id);
        Task<Product> UpdateProduct(Product product);
        Task<Product> GetProductByIdAsync(int id);
        //Task<IEnumerable<Product>> GetProductsWithCategory(Category category);
    }
}
