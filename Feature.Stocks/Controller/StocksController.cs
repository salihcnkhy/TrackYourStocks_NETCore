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
        public Task<GetStocksResponse> AllStocks([FromBody] GetStocksRequest request)
        {
            return SendAsyncRequest<GetStocksRequest, GetStocksResponse, GetAllStocksHandler>(request);
        }

        [HttpPost("GetStockDetail")]
        public Task<GetStockDetailResponse> GetStockDetail([FromBody] GetStockDetailRequest request)
        {
            return SendAsyncRequest<GetStockDetailRequest, GetStockDetailResponse, GetStockDetailHandler>(request);
        }
    }
}
