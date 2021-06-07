using Domain.Assets.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Assets.Model
{
    public class GetAssetInformationPortfolioValueObject
    {
        public string Code { get; set; }
        public int StockQuantity { get; set; }
        public double UnitPrice { get; set; }
        public GetAssetInformationPortfolioValueObject(PortfolioServiceValueObjectResponse portfolioServiceValue)
        {
            Code = portfolioServiceValue.Code;
            StockQuantity = portfolioServiceValue.StockQuantity;
            UnitPrice = portfolioServiceValue.UnitPrice;
        }
    }
}
