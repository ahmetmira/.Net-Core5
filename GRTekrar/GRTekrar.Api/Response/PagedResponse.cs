using GRTekrar.Api.TokenModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GRTekrar.Api.Response
{
    public class PagedResponse<T> : Response<T> where T : ICollection
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public PagedResponse<T> Ok(T data, int count, int pageNumber, int pageSize, string friendlyMessage = null)
        {
            var totalPages = ((double)count / (double)pageSize);

            return new PagedResponse<T>()
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = count,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalPages)),
                Code = HttpStatusCode.OK,
                Message = "Success",
                FriendlyMessage = friendlyMessage
            };
        }

    }
}
