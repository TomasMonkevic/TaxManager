using System;
using TaxManager.Domain;

namespace TaxManager.Service
{
    public interface ITaxService
    {
        double GetTaxRate(string municipalityName, DateTime day);
        void ScheduleTax(string municipalityName, Tax tax);
    }
}
