using Core.Base;
using Feature.Stocks.Model;
using Feature.Stocks.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Stocks.Handler
{
    public class GetAllStocksHandler : RequestHandler<StocksUseCase>, IRequestHandler<GetStocksRequest, GetStocksResponse>
    {
        public Task<GetStocksResponse> Handle(GetStocksRequest request)
        {
            return Task.FromResult(UseCase.GetAllCachedStocks());
        }
    }
}
