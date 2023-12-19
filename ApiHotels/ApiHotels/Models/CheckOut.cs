using System;
using System.Collections.Generic;

namespace ApiHotels.Models
{
    public partial class CheckOut
    {
        public int IdCheckOut { get; set; }
        public DateTime PaymentDate { get; set; }
        public int TotalCost { get; set; }
        public int CheckInId { get; set; }
        public int UserId { get; set; }


    }
}
