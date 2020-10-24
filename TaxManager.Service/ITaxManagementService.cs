using System;

namespace TaxManager.Service
{
    public interface ITaxManagementService
    {
        double GetTaxRate(string municipalityName, DateTime day);
    }
}
