using Core.Base;
using Domain.Stocks.Helper;
using Domain.Stocks.Model;
using System.Threading.Tasks;

namespace Domain.Stocks.Service
{
    public class StocksService: ApiService
    {
        public async Task<GetStocksServiceResponse> GetAllCachedStocks(GetStocksServiceRequest request)
        {
            StocksHelper stocksHelper = new StocksHelper();
            var cachedStocks = await stocksHelper.GetCachedStocks(request);
            return cachedStocks;
        }
    }
}
