using System;
using System.Collections.Generic;

namespace ApiHotels.Models
{
    public partial class User
    {

        public int IdUser { get; set; }
        public string FirstNameUser { get; set; } 
        public string LastNameUser { get; set; } 
        public string MiddleNameUser { get; set; } 
        public string? EmailUser { get; set; }
        public string? NumberPhone { get; set; }
        public string? PasportSeries { get; set; }
        public string? PasportNumber { get; set; }
        public string LoginUser { get; set; } 
        public string PasswordUser { get; set; } 
        public string? Salt { get; set; }
        public int RoleId { get; set; }

    }
}
