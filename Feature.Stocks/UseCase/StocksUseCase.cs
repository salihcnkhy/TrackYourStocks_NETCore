using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Base;
using Domain.Stocks.Model;
using Domain.Stocks.Service;
using Feature.Stocks.Model;

namespace Feature.Stocks.UseCase
{
    public class StocksUseCase : UseCase<StocksService>
    {
        public async Task<GetStocksResponse> GetAllCachedStocks(GetStocksRequest request)
        {
            var serviceRequest = new GetStocksServiceRequest()
            {
                UserID = request.UserID,
                UserToken = request.UserToken,
                PageSize = request.PageSize,
                StartAfterCode = request.StartAfterCode
            };
            var apiResponse = await Api.GetAllCachedStocks(serviceRequest);
            GetStocksResponse stocksResponse = new GetStocksResponse(apiResponse);
            return stocksResponse;
        }
    }
}
