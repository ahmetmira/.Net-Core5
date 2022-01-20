using GRTekrar.Api.Requests.ProductServis.Commands.CreateProduct;
using GRTekrar.Api.Requests.ProductServis.Queries.GelAllProducts;
using GRTekrar.Api.TokenModels;
using GRTekrar.Buisness.Abstract;
using GRTrkrar.Entities.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRTekrar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductServices productServices;
        private readonly IMediator _mediator;

        public ProductController(IProductServices productServices, IMediator mediator)
        {
            this.productServices = productServices;
            _mediator = mediator;

        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll([FromQuery] GelAllProductsQuery query)
        
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromQuery] CreateProductQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Product product)
        {
            await productServices.UpdateProduct(product);
            return Ok(product);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            productServices.DeleteProduct(id);
            return Ok();
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var product = productServices.GetProductById(id);
            return Ok(product);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var product = await productServices.GetProductByIdAsync(id);
            return Ok(product);
        }

        //[HttpGet("GetProductsWithCategory")]
        //public async Task<ActionResult<IEnumerable<Product>>> GetProductsWithCategory(Category category)
        //{
        //    var prwcat = await productServices.GetProductsWithCategory(category);
        //    return Ok(prwcat);
        //}
    }
}
