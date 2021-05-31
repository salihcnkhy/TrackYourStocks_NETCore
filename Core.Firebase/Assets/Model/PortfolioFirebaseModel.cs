using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Firebase.Assets.Model
{
    [FirestoreData]
    public class PortfolioFirebaseModel
    {
        [FirestoreProperty("bought_stock_quantity")]
        public int StockQuantity { get; set; }

        [FirestoreProperty("unit_bought_price")]
        public double UnitPrice { get; set; }

        [FirestoreProperty("code")]
        public string Code { get; set; }
    }
}
