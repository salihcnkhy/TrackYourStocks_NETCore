using Core.Base;
using Domain.Stocks.Helper;
using Domain.Stocks.Model;

namespace Domain.Stocks.Service
{
    public class StocksService: ApiService
    {
        public GetStocksServiceResponse GetCachedStocks(GetStocksServiceRequest request)
        {
            StocksHelper stocksHelper = new StocksHelper();
            var cachedStocks = stocksHelper.GetCachedStocks(request);
            return cachedStocks;
        }

        public GetStocksServiceResponse GetAllCachedStocks()
        {
            StocksHelper stocksHelper = new StocksHelper();
            var cachedStocks = stocksHelper.GetCachedStocks();
            return cachedStocks;
        }
    }
}
