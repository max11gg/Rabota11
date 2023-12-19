using System;
using System.Collections.Generic;

namespace WpfHotel.Models
{
    public partial class CheckIn
    {

        public int IdCheckIn { get; set; }
        public string StatusCheckIn { get; set; } 
        public int UserId { get; set; }
        public int BookingId { get; set; }

    }
}
