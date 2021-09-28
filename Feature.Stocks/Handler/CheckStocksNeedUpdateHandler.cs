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
    public class CheckStocksNeedUpdateHandler : RequestHandler<StocksUseCase>, IRequestHandler<CheckStocksNeedUpdateRequest, CheckStocksNeedUpdateResponse>
    {
        public Task<CheckStocksNeedUpdateResponse> Handle(CheckStocksNeedUpdateRequest request)
        {
            return Task.FromResult(UseCase.CheckStockListNeedUpdate(request));
        }
    }
}
