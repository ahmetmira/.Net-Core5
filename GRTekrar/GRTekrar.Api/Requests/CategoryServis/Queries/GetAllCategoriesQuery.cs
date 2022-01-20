using GRTekrar.Api.Response;
using GRTrkrar.Entities.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRTekrar.Api.Requests.CategoryServis.Queries
{
    public class GetAllCategoriesQuery : PaginationFilter, IRequest<PagedResponse<List<Category>>>
    {
        public string Name { get; set; }
        public string productName { get; set; }
    }
}
