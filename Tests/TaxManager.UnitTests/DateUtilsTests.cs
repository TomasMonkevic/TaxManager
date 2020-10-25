using System;
using Shouldly;
using TaxManager.Service.Utils;
using Xunit;

namespace TaxManager.UnitTests
{
    public class DateUtilsTests
    {
        [Theory]
        [InlineData("2020-10-26", "2020-11-02", "2020-10-25", false)]
        [InlineData("2020-10-26", "2020-11-02", "2020-10-26", true)]
        [InlineData("2020-10-26", "2020-11-02", "2020-11-01", true)]
        [InlineData("2020-10-26", "2020-11-02", "2020-11-02", false)]
        [InlineData("2020-01-01", "2020-01-01", "2020-01-01", false)]
        [InlineData("2020-01-01", "2020-01-02", "2020-01-01", true)]
        public void CanCheckIsDateInclusive(string from, string to, string date, bool expected) 
        {
            var actual = DateUtils.IsDateInclusive(DateTime.Parse(from), DateTime.Parse(to), DateTime.Parse(date));
            actual.ShouldBe(expected);
        }

        [Theory]
        [InlineData("2020-10-26", "2020-11-02", "2020-10-29", "2020-11-03", true)]
        [InlineData("2020-10-26", "2020-11-28", "2020-11-29", "2020-12-03", false)]
        [InlineData("2020-10-26", "2020-11-28", "2020-10-20", "2020-12-03", true)]
        [InlineData("2020-10-26", "2020-10-27", "2020-10-26", "2020-10-27", true)]
        [InlineData("2020-10-26", "2020-10-27", "2020-10-27", "2020-10-28", false)]
        public void CanCheckAreDatesOverlapping(string from, string to, string from2, string to2, bool expected) 
        {
            var actual = DateUtils.AreDatesOverlapping(DateTime.Parse(from), 
                                                       DateTime.Parse(to), 
                                                       DateTime.Parse(from2), 
                                                       DateTime.Parse(to2));
            actual.ShouldBe(expected);
        }
    }
}
