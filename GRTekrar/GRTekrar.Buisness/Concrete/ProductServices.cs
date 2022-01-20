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
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            await unitOfWork.Product.AddAsync(product);
            await unitOfWork.CommitAsync();
            return product;
        }

        public void DeleteProduct(int id)
        {
            unitOfWork.Product.RemoveAsync(id);
            unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await unitOfWork.Product.GetAllAsync();
        }

        public Product GetProductById(int id)
        {
            return unitOfWork.Product.GetById(id);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await unitOfWork.Product.GetByIdAsync(id);
        }

        //public async Task<IEnumerable<Product>> GetProductsWithCategory(Category category)
        //{
        //    return await unitOfWork.Product.GetProductsWithCategory(category);
        //}

        public async Task<Product> UpdateProduct(Product product)
        {
            unitOfWork.Product.UpdateAsync(product);
            await unitOfWork.CommitAsync();
            return product;
        }
    }
}
