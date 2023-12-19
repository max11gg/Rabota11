using System;
using System.Collections.Generic;

namespace ApiHotels.Models
{
    public partial class Service
    {
        public int IdService { get; set; }
        public string NameServices { get; set; } 
        public string DescriptionServices { get; set; } 
        public int PriceServices { get; set; }

    }
}
