using System;

namespace TaxManager.Service.Utils
{
    internal static class DateUtils
    {
        public static bool IsDateInclusive(DateTime from, DateTime to, DateTime date) 
        {
            return from.Date <= date.Date && date.Date < to.Date;
        }
    }
}
