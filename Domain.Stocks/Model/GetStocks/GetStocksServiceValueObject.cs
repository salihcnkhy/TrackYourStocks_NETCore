using Cache.Stocks;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Stocks.Model
{
    public class GetStocksServiceValueObject
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public double CurrentBuying { get; set; }
        public double CurrentSelling { get; set; }
        public double CurrentChange { get; set; }
        public double CurrentChangeRate { get; set; }
        public double DayMax { get; set; }
        public double DayMin { get; set; }
        public List<StockDayInfoServiceValueObject> StocksDayServiceValueObjectList { get; set; }

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
            StocksDayServiceValueObjectList = cacheModel.StockDayCacheModelList.Select(m => new StockDayInfoServiceValueObject()
            {
                Day = m.Day,
                LastBuying = m.LastBuying,
                LastSelling = m.LastSelling,
            }).ToList();
        }
    }

    public class StockDayInfoServiceValueObject
    {
        public double LastBuying { get; set; }
        public double LastSelling { get; set; }
        public string Day { get; set; }
    }
}
