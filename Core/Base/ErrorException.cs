using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
    public class ErrorException : Exception
    {
        public ExceptionType ExceptionType { get; set; }

        public ErrorException(ExceptionType exceptionType) : base(exceptionType.GetMessage())
        {
            ExceptionType = exceptionType;
        }
    }
}
