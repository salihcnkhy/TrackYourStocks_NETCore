using Core.Base;
using Domain.Stocks.Helper;
using Domain.Stocks.Model;
using Domain.Stocks.Model.GetStockDetail;
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

        public async Task<GetStockDayInfoServiceResponse> GetStockPastDateInformation(GetStockDayInfoServiceRequest request)
        {
            StocksHelper stocksHelper = new StocksHelper();
            var stockDayInfo = await stocksHelper.GetStockDayInformation(request);
            return stockDayInfo;
        }

        public async Task<GetStockDetailServiceResponse> GetStockDetail(GetStockDetailServiceRequest request)
        {
            StocksHelper stocksHelper = new StocksHelper();
            return null;
        }

        public bool CheckStockListNeedUpdate(string clientLastUpdate)
        {
            StocksHelper stocksHelper = new StocksHelper();
            return stocksHelper.CheckStockListNeedUpdate(clientLastUpdate);
        }
    }
}
