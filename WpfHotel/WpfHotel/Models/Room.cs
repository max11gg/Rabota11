using System;
using System.Collections.Generic;

namespace WpfHotel.Models
{
    public partial class Room
    {
        public int IdRoom { get; set; }
        public int CountRoom { get; set; }
        public int Price { get; set; }
        public int NumberRoom { get; set; }
        public string TypeRoom { get; set; } 
        public int StatusId { get; set; }
        public int HotelsId { get; set; }

    }
}
