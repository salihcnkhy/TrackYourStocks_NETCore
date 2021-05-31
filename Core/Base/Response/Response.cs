using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Base
{
    public abstract class Response: IResponse
    {
        public bool Success { get; set; }

    }
}
