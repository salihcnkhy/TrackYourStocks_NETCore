﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
    public interface IResponse
    {
        public bool IsSuccess { get; set; }
    }
}
