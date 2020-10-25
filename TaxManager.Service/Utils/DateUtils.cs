using System;

namespace TaxManager.Service.Utils
{
    public static class DateUtils
    {
        public static bool IsDateInclusive(DateTime from, DateTime to, DateTime date) 
        {
            return from.Date <= date.Date && date.Date < to.Date;
        }

        public static bool AreDatesOverlapping(DateTime from1, DateTime to1, DateTime from2, DateTime to2) 
        {
            return from1.Date < to2.Date && from2.Date < to1.Date;
        }
    }
}
