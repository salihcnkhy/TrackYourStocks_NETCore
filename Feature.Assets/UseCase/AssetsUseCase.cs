using Core.Base;
using Core.Extensions;
using Domain.Assets.Model;
using Domain.Assets.Service;
using Domain.Stocks.Service;
using Domain.Stocks.Model;
using Feature.Assets.Model.GetAsset;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Feature.Assets.Model;
using System;
using Core.Firebase;

namespace Feature.Assets.UseCase
{
    public class AssetsUseCase : UseCase<AssetsService>
    {
        public async Task<GetAssetInformationResponse> GetAssetInformation(GetAssetInformationRequest request)
        {
            if (request.UserID.IsNullOrEmpty() || request.UserToken.IsNullOrEmpty())
            {
                return new GetAssetInformationResponse();
            }

            var stockService = new StocksService();

            var serviceResponse = await Api.GetAssetInformation(new GetAssetInformationServiceRequest() { UserID = request.UserID, UserToken = request.UserToken });
            var stocks = await stockService.GetAllCachedStocks(new GetStocksServiceRequest() { UserID = request.UserID, UserToken = request.UserToken });

            if (serviceResponse != null || stocks != null)
            {

                var totalCurrentAsset = 0.0;
                var totalOneDayBeforeAsset = 0.0;
                var totalOneWeekBeforeAsset = 0.0;
                var totalOneMounthBeforeAsset = 0.0;
                var totalBoughtPrice = 0.0;

                // Current Asset
                foreach (var portfolioStock in serviceResponse.PortfolioFirebaseModelList)
                {
                    var currentStock = stocks.ValueObjects.First(s => s.Code == portfolioStock.Code);
                    if (currentStock == null) continue;

                    var oneDayBeforeDate = DateTime.Parse(FirebaseHelper.Shared.FirebaseDateStr).AddDays(-1);
                    var oneWeekBeforeDate = DateTime.Parse(FirebaseHelper.Shared.FirebaseDateStr).AddDays(-7);
                    var oneMountBeforeDate = DateTime.Parse(FirebaseHelper.Shared.FirebaseDateStr).AddMonths(-1);

                    var availableDates = FirebaseHelper.Shared.AvailableDates;

                    oneDayBeforeDate = oneDayBeforeDate.GetNearestPastAvailableDate(availableDates);
                    oneWeekBeforeDate = oneWeekBeforeDate.GetNearestPastAvailableDate(availableDates);
                    oneMountBeforeDate = oneMountBeforeDate.GetNearestPastAvailableDate(availableDates);


                    var oneDayBeforeRequest = new GetStockDayInfoServiceRequest
                    {
                        Code = portfolioStock.Code,
                        Date = oneDayBeforeDate.ToString("yyyy-MM-dd"),
                    };

                    var oneWeekBeforeRequest = new GetStockDayInfoServiceRequest
                    {
                        Code = portfolioStock.Code,
                        Date = oneWeekBeforeDate.ToString("yyyy-MM-dd"),
                    };

                    var oneMounthBeforeRequest = new GetStockDayInfoServiceRequest
                    {
                        Code = portfolioStock.Code,
                        Date = oneMountBeforeDate.ToString("yyyy-MM-dd"),
                    };

                    var oneDayBeforeStockInfo = await stockService.GetStockPastDateInformation(oneDayBeforeRequest);
                    var oneWeekBeforeStockInfo = await stockService.GetStockPastDateInformation(oneWeekBeforeRequest);
                    var oneMounthBeforeStockInfo = await stockService.GetStockPastDateInformation(oneMounthBeforeRequest);


                    totalCurrentAsset += portfolioStock.StockQuantity * currentStock.CurrentSelling;
                    totalOneDayBeforeAsset += portfolioStock.StockQuantity * oneDayBeforeStockInfo.LastSelling;
                    totalOneWeekBeforeAsset += portfolioStock.StockQuantity * oneWeekBeforeStockInfo.LastSelling;
                    totalOneMounthBeforeAsset += portfolioStock.StockQuantity * oneMounthBeforeStockInfo.LastSelling;

                    totalBoughtPrice += portfolioStock.StockQuantity * portfolioStock.UnitPrice;
                }

                var assetProfitList = new List<AssetProfitInformation>();

                var currentProfit = totalCurrentAsset - totalBoughtPrice;
                var currentProfitRate =  (100 *currentProfit) / (totalBoughtPrice == 0 ? 1.0 : totalBoughtPrice);

                var oneDayBeforeProfit = totalOneDayBeforeAsset - totalBoughtPrice;
                var oneDayBeforeProfitRate =  (100 * oneDayBeforeProfit) / (totalBoughtPrice == 0 ? 1.0 : totalBoughtPrice);

                var oneWeekBeforeProfit = totalOneWeekBeforeAsset - totalBoughtPrice;
                var oneWeekBeforeProfitRate = (100 * oneWeekBeforeProfit) / (totalBoughtPrice == 0 ? 1.0 : totalBoughtPrice);

                var oneMounthBeforeProfit = totalOneMounthBeforeAsset - totalBoughtPrice;
                var oneMounthBeforeProfitRate = (100 * oneMounthBeforeProfit) / (totalBoughtPrice == 0 ? 1.0 : totalBoughtPrice);

                var currentAssetProfitInformation = new AssetProfitInformation
                {
                    Title = "Şu an",
                    ProfitRate = Math.Round(currentProfitRate, 2, MidpointRounding.AwayFromZero),
                    ProfitValue = Math.Round(currentProfit, 2, MidpointRounding.AwayFromZero),
                };
                var oneDayBeforeAssetProfitInformation = new AssetProfitInformation
                {
                    Title = "Dün",
                    ProfitRate = Math.Round(oneDayBeforeProfitRate, 2, MidpointRounding.AwayFromZero),
                    ProfitValue = Math.Round(oneDayBeforeProfit, 2, MidpointRounding.AwayFromZero),
                };
                var oneWeekAssetProfitInformation = new AssetProfitInformation
                {
                    Title = "1 Hafta",
                    ProfitRate = Math.Round(oneWeekBeforeProfitRate, 2, MidpointRounding.AwayFromZero),
                    ProfitValue = Math.Round(oneWeekBeforeProfit, 2, MidpointRounding.AwayFromZero),
                };
                var oneMounthAssetProfitInformation = new AssetProfitInformation
                {
                    Title = "1 Ay",
                    ProfitRate = Math.Round(oneMounthBeforeProfitRate, 2, MidpointRounding.AwayFromZero),
                    ProfitValue = Math.Round(oneMounthBeforeProfit, 2, MidpointRounding.AwayFromZero),
                };

                assetProfitList.Add(currentAssetProfitInformation);
                assetProfitList.Add(oneDayBeforeAssetProfitInformation);
                assetProfitList.Add(oneWeekAssetProfitInformation);
                assetProfitList.Add(oneMounthAssetProfitInformation);

                var assetStockInformationList = serviceResponse.PortfolioFirebaseModelList.Select(portfolioStock =>
                {
                    var currentStock = stocks.ValueObjects.First(s => s.Code == portfolioStock.Code);
                    if (currentStock == null) return null;

                    return new AssetStockInformationModel
                    {
                        Code = portfolioStock.Code,
                        Quantity = portfolioStock.StockQuantity,
                        UnitPrice = Math.Round(portfolioStock.UnitPrice, 2, MidpointRounding.AwayFromZero),
                        ProfitRate = Math.Round(100 * ((currentStock.CurrentSelling / portfolioStock.UnitPrice) - 1), 2, MidpointRounding.AwayFromZero),
                        ProfitValue = Math.Round(currentStock.CurrentSelling * portfolioStock.StockQuantity - portfolioStock.UnitPrice * portfolioStock.StockQuantity, 2, MidpointRounding.AwayFromZero),
                        TotalValue = Math.Round(currentStock.CurrentSelling * portfolioStock.StockQuantity, 2, MidpointRounding.AwayFromZero),
                        RateOfStockPrice = Math.Round(100 * (currentStock.CurrentSelling * portfolioStock.StockQuantity) / totalCurrentAsset, 2, MidpointRounding.AwayFromZero),
                        BoughtPrice = Math.Round(portfolioStock.UnitPrice * portfolioStock.StockQuantity, 2, MidpointRounding.AwayFromZero),
                    };
                }).Where(item => item != null).ToList();

                return new GetAssetInformationResponse
                {
                    AssetProfitInformations = assetProfitList,
                    TotalCurrentAsset = Math.Round(totalCurrentAsset, 2, MidpointRounding.AwayFromZero),
                    AssetStockInformations = assetStockInformationList,
                    IsSuccess = true,
                };
            }
            else
                return new GetAssetInformationResponse();
        }
    }
}
