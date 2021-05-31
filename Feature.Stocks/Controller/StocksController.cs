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
        [HttpPost("GetStocks")]
        public Task<GetStocksResponse> GetStocks([FromBody] GetStocksRequest request)
        {
            return SendAsyncRequest<GetStocksRequest, GetStocksResponse, GetStocksHandler>(request);
        }

        [HttpGet("AllStocks")]
        public Task<GetStocksResponse> AllStocks()
        {
            return SendAsyncRequest<GetStocksRequest, GetStocksResponse, GetAllStocksHandler>(null);
        }
    }
}
