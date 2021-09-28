﻿using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Assets.Model
{
    public class PortfolioStockServiceRequest : AuthRequiredRequest 
    {
        public string Code { get; set; }
    }
}
