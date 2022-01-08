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
        public SubErrorType SubErrorType { get; set; }
        public ErrorException(ExceptionType exceptionType, SubErrorType subErrorType = SubErrorType.defaultCode) : base(exceptionType.GetMessage(subErrorType))
        {
            ExceptionType = exceptionType;
            SubErrorType = subErrorType;
        }

        public ErrorException(string message, SubErrorType subErrorType = SubErrorType.defaultCode) : base(message)
        {
            ExceptionType = ExceptionType.undefined;
            SubErrorType = subErrorType;
        }
    }
}
