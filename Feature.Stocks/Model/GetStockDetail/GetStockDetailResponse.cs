using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Stocks.Model
{
    public class GetStockDetailResponse : Response
    {
        public GetStocksValueObject StocksValueObject { get; set; }
    }
}
