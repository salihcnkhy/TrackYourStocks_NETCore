﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Stocks.Model
{
    public class GetStocksServiceRequest
    {
        public string StartAfterCode { get; set; }
        public int PageSize { get; set; }
    }
}
