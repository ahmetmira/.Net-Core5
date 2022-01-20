using GRTekrar.Api.TokenModels;
using GRTrkrar.Entities.Model;
using MediatR;

namespace GRTekrar.Api.Requests.ProductServis.Commands.CreateProduct
{
    public class CreateProductQuery : IRequest<Response<Product>>
    {
        public string Name { get; set; }
        public int? Stock { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public int CategoryId { get; set; }
    }
}
