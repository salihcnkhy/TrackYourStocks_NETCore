using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Assets.Model
{
    public class MarketHistoryListResponse : Response
    {
        public List<MarketHistoryModel> MarkerHistoryList { get; set; }
    }
}
