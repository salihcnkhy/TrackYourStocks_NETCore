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
        public GetStocksResponse GetCachedStocks(GetStocksRequest request)
        {
            GetStocksServiceRequest serviceRequest = new GetStocksServiceRequest { StartAfterCode = request.StartAfterCode, PageSize = request.PageSize };
            var apiResponse = Api.GetCachedStocks(serviceRequest);
            GetStocksResponse stocksResponse = new GetStocksResponse(apiResponse);
            return stocksResponse;
        }

        public GetStocksResponse GetAllCachedStocks()
        {
            var apiResponse = Api.GetAllCachedStocks();
            GetStocksResponse stocksResponse = new GetStocksResponse(apiResponse);
            return stocksResponse;
        }
    }
}
