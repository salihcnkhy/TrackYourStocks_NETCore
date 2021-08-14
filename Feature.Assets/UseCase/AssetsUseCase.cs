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
                if (serviceResponse.PortfolioFirebaseModelList.Count == 0)
                {
                    return new GetAssetInformationResponse();
                }

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

                    var oneDayBeforeRequest = new GetStockDayInfoServiceRequest
                    {
                        Code = portfolioStock.Code,
                        Date = (DateTime.Parse(FirebaseHelper.Shared.FirebaseDate).AddDays(-1)).ToString("yyyy-MM-dd"),
                    };

                    var oneWeekBeforeRequest = new GetStockDayInfoServiceRequest
                    {
                        Code = portfolioStock.Code,
                        Date = (DateTime.Parse(FirebaseHelper.Shared.FirebaseDate).AddDays(-7)).ToString("yyyy-MM-dd"),
                    };

                    var oneMounthBeforeRequest = new GetStockDayInfoServiceRequest
                    {
                        Code = portfolioStock.Code,
                        Date = (DateTime.Parse(FirebaseHelper.Shared.FirebaseDate).AddMonths(-1)).ToString("yyyy-MM-dd"),
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

                if (totalBoughtPrice != 0)
                {
                    var currentProfit = totalCurrentAsset - totalBoughtPrice;
                    var currentProfitRate = 100 * (totalCurrentAsset - totalBoughtPrice) / totalBoughtPrice;

                    var oneDayBeforeProfit = totalOneDayBeforeAsset - totalBoughtPrice;
                    var oneDayBeforeProfitRate = 100 * (totalOneDayBeforeAsset - totalBoughtPrice) / totalBoughtPrice;

                    var oneWeekBeforeProfit = totalOneWeekBeforeAsset - totalBoughtPrice;
                    var oneWeekBeforeProfitRate = 100 * (totalOneWeekBeforeAsset - totalBoughtPrice) / totalBoughtPrice;

                    var oneMounthBeforeProfit = totalOneMounthBeforeAsset - totalBoughtPrice;
                    var oneMounthBeforeProfitRate = 100 * (totalOneMounthBeforeAsset - totalBoughtPrice) / totalBoughtPrice;

                    var currentAssetProfitInformation = new AssetProfitInformation
                    {
                        Title = "Toplam",
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
                }

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
