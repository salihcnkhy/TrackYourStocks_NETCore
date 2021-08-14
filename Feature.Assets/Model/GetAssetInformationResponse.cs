using Core.Base;
using Domain.Assets.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Assets.Model.GetAsset
{
    public class GetAssetInformationResponse : Response
    {
        public List<AssetStockInformationModel> AssetStockInformations { get; set; }
        public List<AssetProfitInformation> AssetProfitInformations { get; set; }
        public double TotalCurrentAsset { get; set; }

        public GetAssetInformationResponse()
        {
            AssetStockInformations = new List<AssetStockInformationModel>();
            AssetProfitInformations = new List<AssetProfitInformation>();
        }
    }

    public class AssetProfitInformation
    {
        public string Title { get; set; }
        public double ProfitRate { get; set; }
        public double ProfitValue { get; set; }
    }
}
