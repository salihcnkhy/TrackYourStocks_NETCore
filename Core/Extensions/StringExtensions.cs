using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class StringExtensions
    { 
        public static bool IsNullOrEmpty(this string value)
        {
            return value == null || value.Equals("");
        }
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !(value == null || value.Equals(""));
        }
    }
}
