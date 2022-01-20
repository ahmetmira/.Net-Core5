using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRTekrar.Api.Requests.ProductServis.Queries.GelAllProducts
{
    public class GetAllProductsValidator : AbstractValidator<GelAllProductsQuery>
    {
        public GetAllProductsValidator()
        {
    
        }
    }
}
