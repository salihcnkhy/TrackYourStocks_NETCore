using Cache.Stocks;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Firebase.Model
{
    [FirestoreData]
    public class StockFirebaseModel
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

        public List<StockDayFirebaseModel> StockDayFirebaseModelList { get; set; }

        public List<StockProfitDayModel> StockProfitDayFirebaseModel { get; set; }

        public StockCacheModel GetStockCacheModel()
        {
            return new StockCacheModel()
            {
                Code = Code,
                CurrentBuying = CurrentBuying,
                CurrentChange = CurrentChange,
                CurrentChangeRate = CurrentChangeRate,
                CurrentSelling = CurrentSelling,
                DayMax = DayMax,
                DayMin = DayMin,
                FullName = FullName,
                ShortName = ShortName,
                StockDayCacheModelList = StockDayFirebaseModelList.Select(m => new StockDayCacheModel()
                {
                    Day = m.Day,
                    LastBuying = m.LastBuying,
                    LastSelling = m.LastSelling
                }).ToList(),
                StockProfitDayModeList = StockProfitDayFirebaseModel.Select(m => new StockProfitDayModel()
                {
                    Protif = m.Protif,
                    ProtifRate = m.ProtifRate,
                    Title = m.Title,
                }).ToList()
            };
        }
    }

    [FirestoreData]
    public class StockDayFirebaseModel
    {
        [FirestoreProperty("last_buying")]
        public double LastBuying { get; set; }

        [FirestoreProperty("last_selling")]
        public double LastSelling { get; set; }

        [FirestoreProperty("day")]
        public string Day { get; set; }
    }

    public class StockProfitDayModel
    {
        public double Protif { get; set; }
        public double ProtifRate { get; set; }
        public string Title { get; set; }
    }
}
