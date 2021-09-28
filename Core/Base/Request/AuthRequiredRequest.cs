using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
    public abstract class AuthRequiredRequest : Request
    {
        public string UserToken { get; set; }
        public string UserID { get; set; }
    }
}
