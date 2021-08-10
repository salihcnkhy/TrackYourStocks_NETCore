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
        public List<GetAssetInformationPortfolioValueObject> PortfolioValueObjects { get; set; }
        public double TotalBoughtPrice { get; set; }
        public double TotalCurrentAsset { get; set; }
        public double CurrentProfit { get; set; }
        public double CurrentProfitRate { get; set; }
        public GetAssetInformationResponse(GetAssetInformationServiceResponse serviceResponse)
        {
            PortfolioValueObjects = serviceResponse.PortfolioServiceValues.Select(item => new GetAssetInformationPortfolioValueObject(item)).ToList();
            TotalBoughtPrice = serviceResponse.TotalBoughtPrice;
            TotalCurrentAsset = serviceResponse.TotalCurrentAsset;
            CurrentProfit = serviceResponse.CurrentProfit;
            CurrentProfitRate = serviceResponse.CurrentProfitRate;
            IsSuccess = true;
        }

        public GetAssetInformationResponse()
        {
            IsSuccess = false;
        }
    }
}
