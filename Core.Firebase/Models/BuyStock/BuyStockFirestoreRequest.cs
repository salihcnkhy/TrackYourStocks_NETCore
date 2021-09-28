using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Service.Models
{
    public class BuyStockFirestoreRequest : FirestoreGeneralRequest
    {
        public string Code { get; set; }
        public int AdditionalBuyQuantity { get; set; }
        public double CurrentBoughtPrice { get; set; }
        public double NewUnitBoughtPrice { get; set; }
    }
}
