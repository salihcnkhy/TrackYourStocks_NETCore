using Domain.Stocks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Stocks.Model
{
   public class GetStocksValueObject
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
        public List<StockDayInfoValueObject> StockDayInformationList { get; set; }
        public List<StockProfitDayModel> StockProfitDayModelList { get; set; }

        public GetStocksValueObject(GetStocksServiceValueObject serviceValueObject)
        {
            Code = serviceValueObject.Code;
            FullName = serviceValueObject.FullName;
            ShortName = serviceValueObject.ShortName;
            CurrentBuying = serviceValueObject.CurrentBuying;
            CurrentSelling = serviceValueObject.CurrentSelling;
            CurrentChange = serviceValueObject.CurrentChange;
            CurrentChangeRate = serviceValueObject.CurrentChangeRate;
            DayMax = serviceValueObject.DayMax;
            DayMin = serviceValueObject.DayMin;
            StockDayInformationList = serviceValueObject.StocksDayServiceValueObjectList.Select(m => new StockDayInfoValueObject()
            {
                Day = m.Day,
                LastBuying = m.LastBuying,
                LastSelling = m.LastSelling,
            }).ToList();

            StockProfitDayModelList = serviceValueObject.StockProfitDayModelList;
        }
    }

    public class StockDayInfoValueObject
    {
        public double LastBuying { get; set; }
        public double LastSelling { get; set; }
        public string Day { get; set; }
    }
}
