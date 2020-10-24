using System;

namespace TaxManager.Service
{
    public interface ITaxService
    {
        double GetTaxRate(string municipalityName, DateTime day);
    }
}
