using GRTekrar.Api.Response;
using GRTekrar.DataAccess.Abstract;
using GRTrkrar.Entities.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GRTekrar.Api.Requests.CategoryServis.Queries
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, PagedResponse<List<Category>>>
    {
        private readonly IUnitOfWork _unitOfWork;


        public GetAllCategoriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<PagedResponse<List<Category>>> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
        {
            var totalProducts = await _unitOfWork.Category.GetAllAsync();
            ////var list  = _mapper.Map<IdNameResponse>(products);

            var list = await _unitOfWork.Category.GetAllCategories(query.productName,query.PageNumber, query.PageSize);
            if (query.Name != null)
                list = list.Where(x => x.Name.ToLower().Contains(query.Name.ToLower())).ToList();

            //List olsaydı .count parantez yazmamız dpğru değil.
            return new PagedResponse<List<Category>>().Ok(list, totalProducts.Count(), query.PageNumber, query.PageSize);
        }
    }

}

