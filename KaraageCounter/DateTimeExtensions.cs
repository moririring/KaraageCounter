using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DateTimeExtensions
{
    static public class DateTimeExtensions
    {
        public static bool IsToday(this DateTime date)
        {
            var now = DateTime.Now;
            return (date.Year == now.Year && date.Month == now.Month && date.Day == now.Day);
        }
        public static bool IsYesterday(this DateTime date)
        {
            var now = DateTime.Now;
            return (date.Year == now.Year && date.Month == now.Month && date.Day+1 == now.Day);
        }
    }
}