using System;
using Shouldly;
using TaxManager.Persistence.Repository;
using TaxManager.Service;
using TaxManager.Domain;
using NSubstitute;
using Xunit;
using System.Collections.Generic;
using TaxManager.Service.Exceptions;

namespace TaxManager.UnitTests
{
    public class TaxServiceTests
    {
        private readonly IMunicipalityRepository _municipalityRepo = Substitute.For<IMunicipalityRepository>();
        private readonly ITaxRepository _taxRepository = Substitute.For<ITaxRepository>();

        private readonly TaxService _taxService;

        public TaxServiceTests() 
        {
            _taxService = new TaxService(_municipalityRepo, _taxRepository);
        }

        [Theory]
        [MemberData(nameof(CanGetTaxRateData))]
        public void CanGetTaxRate(List<Tax> taxes, DateTime day, double expectedRate)
        {
            var municipality = "Vilnius";
            _municipalityRepo.Get(municipality).Returns(new Municipality {
                Taxes = taxes
            });

            var rate = _taxService.GetTaxRate(municipality, day);
            rate.ShouldBe(expectedRate);
        }

        [Fact]
        public void GetTaxRate_TaxNotApppliedException() {
            Should.Throw<TaxNotAppliedException>(() => 
            _taxService.GetTaxRate("random", DateTime.Now));
        }

        [Fact]
        public void CanSchedule()
        {
            _municipalityRepo.Get("Vilnius").Returns(new Municipality
            {
                Taxes = new List<Tax> {
                    new Tax { TaxType = TaxType.Monthly, From = new DateTime(2020, 10, 1) }
                }
            });
            var isScheduled = _taxService.ScheduleTax("Vilnius", new Tax { TaxType = TaxType.Daily, From = new DateTime(2020, 10, 15) });

            isScheduled.ShouldBe(true);
        }

        [Fact]
        public void Schedule_WhenTaxIsOverlapping()
        {
            _municipalityRepo.Get("Vilnius").Returns(new Municipality
            {
                Taxes = new List<Tax> {
                    new Tax { TaxType = TaxType.Monthly, From = new DateTime(2020, 10, 1) }
                }
            });
            var isScheduled = _taxService.ScheduleTax("Vilnius", new Tax { TaxType = TaxType.Monthly, From = new DateTime(2020, 10, 15) });

            isScheduled.ShouldBe(false);
        }

        [Fact]
        public void Schedule_WhenMunicipalityNotFound()
        {
            var isScheduled = _taxService.ScheduleTax("Vilnius", default);

            isScheduled.ShouldBe(false);
        }

        public static IEnumerable<object[]> CanGetTaxRateData()
        {
            var dailyTax = new Tax {
                Rate = 0.4,
                TaxType = TaxType.Daily,
                From = new DateTime(2020, 1, 15)
            };

            var weeklyTax = new Tax {
                Rate = 0.3,
                TaxType = TaxType.Weekly,
                From = new DateTime(2020, 1, 06)
            };

            var monthlyTax = new Tax {
                Rate = 0.2,
                TaxType = TaxType.Monthly,
                From = new DateTime(2020, 1, 1)
            };

            var annuallTax = new Tax {
                Rate = 0.1,
                TaxType = TaxType.Annually,
                From = new DateTime(2020, 1, 1)
            };

            var allData = new List<object[]>
            {
                new object[] { new List<Tax> { dailyTax }, new DateTime(2020, 1, 15), 0.4 },
                new object[] { new List<Tax> { dailyTax, annuallTax }, new DateTime(2020, 1, 14), 0.1 },
                new object[] { new List<Tax> { dailyTax, annuallTax }, new DateTime(2020, 1, 16), 0.1 },
                new object[] { new List<Tax> { monthlyTax, annuallTax }, new DateTime(2020, 1, 1), 0.2 },
                new object[] { new List<Tax> { monthlyTax, annuallTax }, new DateTime(2020, 1, 31), 0.2 },
                new object[] { new List<Tax> { monthlyTax, annuallTax }, new DateTime(2020, 2, 1), 0.1 },
                new object[] { new List<Tax> { weeklyTax, annuallTax }, new DateTime(2020, 1, 5), 0.1 },
                new object[] { new List<Tax> { weeklyTax, annuallTax }, new DateTime(2020, 1, 6), 0.3 },
                new object[] { new List<Tax> { weeklyTax, annuallTax }, new DateTime(2020, 1, 9), 0.3 },
                new object[] { new List<Tax> { weeklyTax, annuallTax }, new DateTime(2020, 1, 12), 0.3 },
                new object[] { new List<Tax> { weeklyTax, annuallTax }, new DateTime(2020, 1, 13), 0.1 },
            };

            return allData;
        }
    }
}
