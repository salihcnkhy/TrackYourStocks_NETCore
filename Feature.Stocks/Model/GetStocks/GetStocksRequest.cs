using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Stocks.Model
{
    public class GetStocksRequest: AuthRequiredRequest
    {
        public string StartAfterCode { get; set; }
        public int PageSize { get; set; }
    }
}
