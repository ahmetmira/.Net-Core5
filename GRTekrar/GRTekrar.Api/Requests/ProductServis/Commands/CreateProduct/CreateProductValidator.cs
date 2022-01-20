using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRTekrar.Api.Requests.ProductServis.Commands.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductQuery>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(3,100);
            //RuleFor(x => x.Name).MinimumLength(3);

            RuleFor(x => x.Price)
                .NotEmpty()
                .ExclusiveBetween(10, 100000);
        }
    }
}
