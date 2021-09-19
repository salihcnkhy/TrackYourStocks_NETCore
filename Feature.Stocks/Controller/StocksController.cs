using Core.Base;
using Feature.Stocks.Handler;
using Feature.Stocks.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Stocks.Controller
{
    public class StocksController: ApiController
    {
        [HttpPost("AllStocks")]
        public Task<IResponse> AllStocks([FromBody] GetStocksRequest request)
        {
            return SendAsyncRequest<GetStocksRequest, GetStocksResponse, GetAllStocksHandler>(request);
        }

        [HttpPost("GetStockDetail")]
        public Task<IResponse> GetStockDetail([FromBody] GetStockDetailRequest request)
        {
            return SendAsyncRequest<GetStockDetailRequest, GetStockDetailResponse, GetStockDetailHandler>(request);
        }

        [HttpPost("CheckStocksNeedUpdate")]
        public Task<IResponse> CheckStocksNeedUpdate([FromBody] CheckStocksNeedUpdateRequest request)
        {
            return SendAsyncRequest<CheckStocksNeedUpdateRequest, CheckStocksNeedUpdateResponse, CheckStocksNeedUpdateHandler>(request);
        }
    }
}
