using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime GetNearestPastAvailableDate(this DateTime value, List<DateTime> dates)
        {
            var dateList = new List<DateTime>();
            dateList.AddRange(dates);
            dateList.Sort((x, y) => x.CompareTo(y));
            var oldest = dateList.First();

            bool exist = dateList.Any(d => d.Month == value.Month && d.Day == value.Day);
            bool onLimit = oldest.CompareTo(value) == 0;
            while (!exist && !onLimit)
            {
                value = value.AddDays(-1);
                exist = dateList.Any(d => d.Month == value.Month && d.Day == value.Day);
                onLimit = oldest.CompareTo(value) == 0;
            }

            return value;
        }
    }
}
