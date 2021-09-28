using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.Stocks
{
    public class StockCacheModel
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
        public List<StockDayCacheModel> StockDayCacheModelList {get; set; }
    }

    public class StockDayCacheModel
    {
        public double LastBuying { get; set; }
        public double LastSelling { get; set; }
        public string Day { get; set; }
    }
}
