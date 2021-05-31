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
        }
    }
}
