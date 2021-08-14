using Cache.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache
{
    public class StocksCache
    {
        private static StocksCache _shared;
        public static StocksCache Shared
        {
            get
            {
                if(_shared == null)
                {
                    _shared = new StocksCache();
                }
                return _shared;
            }
        }
        private StocksCache() {  }

        public List<StockCacheModel> CachedStocks { get; set; }

    }
}
