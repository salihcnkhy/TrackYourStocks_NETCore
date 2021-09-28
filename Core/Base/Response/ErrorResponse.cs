using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{


    public class ErrorResponse : Response
    {
        public string Message { get; set; }
        public ExceptionType ExceptionType { get; set; }
    }
}
