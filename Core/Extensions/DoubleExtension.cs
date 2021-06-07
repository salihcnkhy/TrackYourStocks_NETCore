using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class DoubleExtension
    {
        public static string ToMoneyString(this double value) => String.Format("{0:0.00} TL", value);

    }

}
