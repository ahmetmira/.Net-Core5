using GRTekrar.Api.Requests.CategoryServis.Queries;
using GRTekrar.Api.TokenModels;
using GRTekrar.Buisness.Abstract;
using GRTrkrar.Entities.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GRTekrar.Api.Controllers
{
    [Route("api/[controller]")]
    //[Produces("application/x-www-form-urlencoded")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryServices categoryServices;
        private readonly IMediator _mediator;

        //private readonly IHttpContextAccessor _httpContext;


        public CategoryController(ICategoryServices categoryServices, IMediator mediator)
        {
            this.categoryServices = categoryServices;
            _mediator = mediator;
            //_httpContext = httpContext;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCategoriesQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }



        [HttpPost("GetAllCategories")]
        //[Produces("application/x-www-form-urlencoded")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var draw =  Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var categories = await categoryServices.GetAllCategory();
                var categoryData = (from tempcategory in categories select tempcategory);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    categoryData = categoryData.AsQueryable().OrderBy(sortColumn + " " + sortColumnDirection);
                    //categoryData = categoryData.OrderBy(w => w.Name == (sortColumn + " " + sortColumnDirection));
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    categoryData = categoryData.Where(m => m.Name.Contains(searchValue));
                }
                recordsTotal = categoryData.Count();
                var data = categoryData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception)
            {
                throw;
                //return new Response<IEnumerable<Category>>().NoContent(ex.ToString());
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Create(string name )

        {
            var categoryObject = new Category
            {
                Name = name
            };
            await categoryServices.CreateCategory(categoryObject);
            return Ok(new IdNameResponse { Id = categoryObject.Id, Name =categoryObject.Name });
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Category category)
        {
            if (categoryServices.GetCategoryByIdAsync(category.Id) != null)
            {
                return Ok(await categoryServices.UpdateCategory(category));
            }
            return NotFound();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (categoryServices.GetCategoryById(id) != null)
            {
                categoryServices.DeleteCategory(id);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var category = categoryServices.GetCategoryById(id);
            return Ok(category);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var category = await categoryServices.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        [HttpGet("GetCategoriesWithProducts")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesWithProducts()
        {
            var catwpr = await categoryServices.GetCategoriesWithProducts();
            return Ok(catwpr);
        }
    }
}
