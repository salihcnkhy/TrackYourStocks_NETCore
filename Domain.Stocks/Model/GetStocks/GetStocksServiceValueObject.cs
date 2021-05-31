using Cache.Stocks;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Stocks.Model
{
    [FirestoreData]
    public class GetStocksServiceValueObject
    {
        [FirestoreProperty("code")]
        public string Code { get; set; }

        [FirestoreProperty("full_name")]
        public string FullName { get; set; }

        [FirestoreProperty("short_name")]
        public string ShortName { get; set; }

        [FirestoreProperty("current_buying")]
        public double CurrentBuying { get; set; }

        [FirestoreProperty("current_selling")]
        public double CurrentSelling { get; set; }

        [FirestoreProperty("current_change")]
        public double CurrentChange { get; set; }

        [FirestoreProperty("current_change_rate")]
        public double CurrentChangeRate { get; set; }

        [FirestoreProperty("day_max")]
        public double DayMax { get; set; }

        [FirestoreProperty("day_min")]
        public double DayMin { get; set; }


        public GetStocksServiceValueObject(StockCacheModel cacheModel)
        {
            Code = cacheModel.Code;
            FullName = cacheModel.FullName;
            ShortName = cacheModel.ShortName;
            CurrentBuying = cacheModel.CurrentBuying;
            CurrentSelling = cacheModel.CurrentSelling;
            CurrentChange = cacheModel.CurrentChange;
            CurrentChangeRate = cacheModel.CurrentChangeRate;
            DayMax = cacheModel.DayMax;
            DayMin = cacheModel.DayMin;
        }
    }
}
