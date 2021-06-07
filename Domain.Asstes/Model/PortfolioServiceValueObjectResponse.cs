using Core.Firebase.Assets.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Assets.Model
{
    public class PortfolioServiceValueObjectResponse
    {
        public string Code { get; set; }
        public int StockQuantity { get; set; }
        public double UnitPrice { get; set; }
        public PortfolioServiceValueObjectResponse(PortfolioFirebaseModel firebaseModel)
        {
            Code = firebaseModel.Code;
            StockQuantity = firebaseModel.StockQuantity;
            UnitPrice = firebaseModel.UnitPrice;
        }
    }
}
