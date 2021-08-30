using Core.Base;
using System.Collections.Generic;

namespace Feature.Assets.Model
{
    public class GetAssetInformationResponse : Response
    {
        public List<AssetStockInformationModel> AssetStockInformations { get; set; }
        public List<AssetProfitInformation> AssetProfitInformations { get; set; }
        public List<MarketHistoryModel> MarketHistories { get; set; }
        public double TotalCurrentAsset { get; set; }

        public GetAssetInformationResponse()
        {
            AssetStockInformations = new List<AssetStockInformationModel>();
            AssetProfitInformations = new List<AssetProfitInformation>();
            MarketHistories = new List<MarketHistoryModel>();
        }
    }

    public class AssetProfitInformation
    {
        public string Title { get; set; }
        public double ProfitRate { get; set; }
        public double ProfitValue { get; set; }
    }
}
