using GRTekrar.Api.TokenModels;
using GRTekrar.DataAccess.Abstract;
using GRTrkrar.Entities.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GRTekrar.Api.Requests.ProductServis.Commands.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductQuery,Response<Product>>
    {
        private readonly IUnitOfWork _unitOfWork;


        public CreateProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<Response<Product>> Handle(CreateProductQuery query, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Product.CheckIfExist(query.Name))
            {
                return new Response<Product>().Conflict();
            }
            var productObject = new Product
            {
                Name = query.Name,
                CategoryId=query.CategoryId,
                //Stock=()query.Stock,
                Stock = query.Stock.GetValueOrDefault(),
                Price=query.Price,
                Weight=query.Weight

            };
            await _unitOfWork.Product.AddAsync(productObject);
        
            return await _unitOfWork.CommitAsync() > 0 ? new Response<Product>().Created(productObject) : new Response<Product>().ServiceUnavailable();


        }
    }
}