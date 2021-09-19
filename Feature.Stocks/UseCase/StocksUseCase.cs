using System.Threading.Tasks;
using Core.Base;
using Domain.Stocks.Model;
using Domain.Stocks.Service;
using Feature.Stocks.Model;

namespace Feature.Stocks.UseCase
{
    public class StocksUseCase : UseCase<StocksService>
    {
        public async Task<GetStocksResponse> GetAllCachedStocks(GetStocksRequest request)
        {
            var serviceRequest = new GetStocksServiceRequest()
            {
                UserID = request.UserID,
                UserToken = request.UserToken,
                PageSize = request.PageSize,
                StartAfterCode = request.StartAfterCode
            };
            var apiResponse = await Api.GetAllCachedStocks(serviceRequest);
            GetStocksResponse stocksResponse = new GetStocksResponse(apiResponse);
            return stocksResponse;
        }

        public async Task<GetStockDetailResponse> GetStockDetail(GetStockDetailRequest request)
        {
            var serviceRequest = new GetStockDetailServiceRequest
            {
                Code = request.Code,
                DayFrequency = request.DayFrequency,
                DayInformationSize = request.DayInformationSize,
                IsProfitDayInfomationRequired = request.IsProfitDayInfomationRequired,
                UserID = request.UserID,
                UserToken = request.UserToken,
            };

            var response = await Api.GetStockDetail(serviceRequest);

            return new GetStockDetailResponse
            {
                StocksValueObject = new GetStocksValueObject(response?.StocksServiceValueObject),
            };
        }
        public CheckStocksNeedUpdateResponse CheckStockListNeedUpdate(CheckStocksNeedUpdateRequest request)
        {
            var response = Api.CheckStockListNeedUpdate(request.ClientUpdateUUID);
            return new CheckStocksNeedUpdateResponse
            {
                IsNeedUpdate = response,
            };
        }
    }
}
