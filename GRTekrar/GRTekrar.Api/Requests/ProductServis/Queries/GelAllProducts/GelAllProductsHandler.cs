using GRTekrar.Api.Response;
using GRTekrar.Api.TokenModels;
using GRTekrar.DataAccess.Abstract;
using GRTrkrar.Entities.Model;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GRTekrar.Api.Requests.ProductServis.Queries.GelAllProducts
{
    public class GelAllProductsHandler : IRequestHandler<GelAllProductsQuery, PagedResponse<List<Product>>>
    {
        private readonly IUnitOfWork _unitOfWork;


        public GelAllProductsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<PagedResponse<List<Product>>> Handle(GelAllProductsQuery query, CancellationToken cancellationToken)
        {
            var totalProducts = await _unitOfWork.Product.GetAllAsync();
            if (query.Name != null)
            {
                totalProducts = totalProducts.Where(x => x.Name.ToLower().Contains(query.Name.ToLower())).ToList();
            }
            ////var list  = _mapper.Map<IdNameResponse>(products);

            var list = await _unitOfWork.Product.GetAllProduct(query.PageNumber, query.PageSize);
            if (query.Name != null )
            list = list.Where(x => x.Name.ToLower().Contains(query.Name.ToLower())).ToList();

            //List olsaydı .count parantez yazmamız dpğru değil.
            return new PagedResponse<List<Product>>().Ok(list, totalProducts.Count(), query.PageNumber, query.PageSize);
        }
    }
}