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
        public List<GetStockDayInfoServiceResponse> StocksDayServiceValueObjectList { get; set; }
        public List<StockProfitDayModel> StockProfitDayModelList { get; set; }
        public GetStocksServiceValueObject(StockCacheModel cacheModel, List<string> userFavoriteStockList)
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
            StocksDayServiceValueObjectList = cacheModel.StockDayCacheModelList.Select(m => new GetStockDayInfoServiceResponse()
            {
                Day = m.Day,
                LastBuying = m.LastBuying,
                LastSelling = m.LastSelling,
            }).ToList();
        }
    }
    public class StockProfitDayModel
    {
        public double Protif { get; set; }
        public double ProtifRate { get; set; }
        public string Title { get; set; }
    }
}
