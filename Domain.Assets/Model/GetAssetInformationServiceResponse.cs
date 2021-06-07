using Core.Cache;
using Core.Extensions;
using Core.Firebase.Assets.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Assets.Model
{
    public class GetAssetInformationServiceResponse
    {

        public List<PortfolioServiceValueObjectResponse> PortfolioServiceValues { get; set; }
        public double TotalBoughtPrice { get; set; }
        public double TotalCurrentAsset { get; set; }
        public double CurrentProfit { get; set; }
        public double CurrentProfitRate { get; set; }
        public GetAssetInformationServiceResponse(List<PortfolioFirebaseModel> firebaseModelList)
        {
            PortfolioServiceValues = firebaseModelList.Select(item => new PortfolioServiceValueObjectResponse(item)).ToList();

            // Current Asset
            TotalCurrentAsset = 0;
            TotalBoughtPrice = 0;
            foreach (var portfolioStock in PortfolioServiceValues)
            {
                var stockCode = portfolioStock.Code;
                var portfolioStockQuantity = portfolioStock.StockQuantity;

                var stock = StocksCache.Shared.CachedStocks.First(stock => stock.Code.Equals(stockCode));
                TotalCurrentAsset += portfolioStockQuantity * stock.CurrentSelling;
                TotalBoughtPrice += portfolioStockQuantity * portfolioStock.UnitPrice;
            }

            // Current Profit
            CurrentProfit = TotalCurrentAsset - TotalBoughtPrice;

            // current profit rate
            CurrentProfitRate = 100 * (TotalCurrentAsset - TotalBoughtPrice) / TotalBoughtPrice;            
        }
    }
}
