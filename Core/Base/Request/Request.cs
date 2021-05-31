using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
    public abstract class Request : IRequest
    {
        public string UserToken { get; set; }
    }
}
