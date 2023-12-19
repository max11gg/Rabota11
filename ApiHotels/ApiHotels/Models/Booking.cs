using System;
using System.Collections.Generic;

namespace ApiHotels.Models
{
    public partial class Booking
    {

        public int IdBooking { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public int RoomId { get; set; }
        public bool IsBooking { get; set; }


    }
}
