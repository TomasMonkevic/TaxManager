﻿using System.Collections.Generic;

namespace TaxManager.Domain
{
    public class Municipality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tax> Taxes { get; set; }
    }
}
