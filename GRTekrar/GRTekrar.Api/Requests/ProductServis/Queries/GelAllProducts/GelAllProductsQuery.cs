using GRTekrar.Api.Response;
using GRTekrar.Api.TokenModels;
using GRTrkrar.Entities.Model;
using MediatR;
using System.Collections.Generic;

namespace GRTekrar.Api.Requests.ProductServis.Queries.GelAllProducts
{
    public class GelAllProductsQuery : PaginationFilter, IRequest<PagedResponse<List<Product>>>
    {
        public string Name { get; set; }
    }
}
