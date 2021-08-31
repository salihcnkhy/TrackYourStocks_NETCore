using Core.Base;
using Core.Firebase;
using Domain.Assets.Model;
using Firebase.Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Assets.Service
{
    public class AssetsService : ApiService
    {
        public async Task<GetAssetInformationServiceResponse> GetAssetInformation(GetAssetInformationServiceRequest request)
        {
            var service = new FirebaseService();
            var firebasePortfolioListResponse = await service.GetUserPortfolioList(new FirestoreGeneralRequest { UserID = request.UserID, UserToken = request.UserToken });
            firebasePortfolioListResponse = firebasePortfolioListResponse.OrderBy(c => c.Code).ToList();
            return new GetAssetInformationServiceResponse() { PortfolioFirebaseModelList = firebasePortfolioListResponse };
        }


        public async Task<List<MarketHistoryServiceModel>> GetMarketHistoryList(MarketHistoryRequest request)
        {
            var service = new FirebaseService();
            var responseList = await service.GetMarketHistory(new FirestoreGeneralRequest { UserID = request.UserID, UserToken = request.UserToken });
            var response = responseList.Select(a => new MarketHistoryServiceModel
            {
                Code = a.Code,
                Date = a.Date.ToDateTime(),
                ProcessType = a.ProcessType,
                Quantity = a.Quantity,
                UnitPrice = a.UnitPrice,
                LongName = a.LongName,
            }).ToList();
            return response;
        }

        public async Task<PortfolioFirebaseModel> GetUserPorfolioStock(PortfolioStockServiceRequest request)
        {
            var service = new FirebaseService();
            return await service.GetUserPortfolioStock(new UserPorfolioStockRequest
            {
                Code = request.Code,
                UserID = request.UserID,
                UserToken = request.UserToken
            });
        }

        public async Task<BuyStockServiceResponse> BuyStock(BuyStockServiceRequest request)
        {
            var service = new FirebaseService();
            await service.BuyStock(new BuyStockFirestoreRequest
            {
                Code = request.Code,
                AdditionalBuyQuantity = request.AdditionalBuyQuantity,
                CurrentBoughtPrice = request.CurrentBoughtPrice,
                NewUnitBoughtPrice = request.NewUnitBoughtPrice,
                UserID = request.UserID,
                UserToken = request.UserToken,
            });
            return new BuyStockServiceResponse { IsSuccess = true };
        }

        public async Task<SellStockServiceResponse> SellStock(SellStockServiceRequest request)
        {
            var service = new FirebaseService();
            await service.SellStock(new SellStockFirestoreRequest
            {
                Code = request.Code,
                LotQuantity = request.LotQuantity,
                SellPrice = request.SellPrice,
                UserID = request.UserID,
                UserToken = request.UserToken,
            });
            return new SellStockServiceResponse { IsSuccess = true };
        }
    }
}
