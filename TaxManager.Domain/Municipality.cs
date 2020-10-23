using System.Collections.Generic;

namespace TaxManager.Domain
{
    public class Municipality
    {
        public string Name { get; set; }
        public List<Tax> Taxes { get; set; }
    }
}
