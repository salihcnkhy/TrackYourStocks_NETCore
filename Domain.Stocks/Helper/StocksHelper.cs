using Core.Cache;
using Domain.Stocks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Stocks.Helper
{
    public class StocksHelper
    {
        public GetStocksServiceResponse GetCachedStocks(GetStocksServiceRequest request = null)
        {
            var cachedStocks = StocksCache.Shared.CachedStocks;
            GetStocksServiceResponse response = null;
            if (cachedStocks != null)
            {
                response = new GetStocksServiceResponse()
                {
                    ValueObjects = cachedStocks.Select(s => new GetStocksServiceValueObject(s)).ToList()
                };
                if (request != null)
                    response = GetFilteredResponse(request, response);
            }
            return response;
        }

        private GetStocksServiceResponse GetFilteredResponse(GetStocksServiceRequest request, GetStocksServiceResponse response)
        {
            var stocksList = response.ValueObjects;
            if (!String.IsNullOrEmpty(request.StartAfterCode))
            {
                stocksList = stocksList.SkipWhile(stock => !stock.Code.Equals(request.StartAfterCode)).ToList();
                stocksList = stocksList.Skip(1).ToList();
            }
            stocksList = stocksList.SkipLast(stocksList.Count - request.PageSize).ToList();
            response.ValueObjects = stocksList;
            return response;
        }
    }
}
