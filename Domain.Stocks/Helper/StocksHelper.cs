using Core.Firebase;
using Core.Firebase.Model;
using Domain.Stocks.Model;
using Firebase.Service.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Stocks.Helper
{
    public class StocksHelper
    {
        public async Task<GetStocksServiceResponse> GetCachedStocks(GetStocksServiceRequest request)
        {
            var firebaseService = new FirebaseService();
            var firebaseRequest = new FirestoreGeneralRequest { UserID = request.UserID, UserToken = request.UserToken };
            var cachedStocks = await firebaseService.GetCachedStocks(firebaseRequest);
            GetStocksServiceResponse response = null;
            if (cachedStocks != null)
            {
                response = new GetStocksServiceResponse()
                {
                    ValueObjects = cachedStocks.Select(s => new GetStocksServiceValueObject(s)).ToList(),
                    ClientUpdateUUID = FirebaseHelper.Shared.LastUpdateUUID,
                };
                if (request != null && request.PageSize > 0)
                    response = GetFilteredResponse(request, response);
            }
            return response;
        }

        public async Task<GetStockDayInfoServiceResponse> GetStockDayInformation(GetStockDayInfoServiceRequest request)
        {
            var firebaseService = new FirebaseService();
            var stockInfoRequest = new StockDayInformationRequest
            {
                Code = request.Code,
                Date = request.Date,
            };

            var response = await firebaseService.GetStockDayInformation(stockInfoRequest);
            if (response == null) return null; // TODO: if returns null try next day maybe??
            return new GetStockDayInfoServiceResponse
            {
                Day = response.Day,
                LastBuying = response.LastBuying,
                LastSelling = response.LastSelling,
            };
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
            response.IsContinue = stocksList.Count == request.PageSize;
            response.ValueObjects = stocksList;
            return response;
        }

        public async Task<GetStockDetailServiceResponse> GetStockDetail(GetStockDetailServiceRequest request)
        {
            var firebaseService = new FirebaseService();
            var response = await firebaseService.GetStockDetail(new StockDetailRequest 
            { 
                Code = request.Code,
                DayFrequency = request.DayFrequency,
                DayInformationSize = request.DayInformationSize,
                IsProfitDayInfomationRequired = request.IsProfitDayInfomationRequired,
                UserToken = request.UserToken,
                UserID = request.UserID,
            });

            return new GetStockDetailServiceResponse { StocksServiceValueObject = new GetStocksServiceValueObject(response) };
        }

        public bool CheckStockListNeedUpdate(string clientUpdateUUID) => clientUpdateUUID != FirebaseHelper.Shared.LastUpdateUUID;
    }
}
