using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Staycation.Models
{
    public partial class Booking
    {
        public int BookingNummer { get; set; }
        public int AntalVoksne { get; set; }
        public int? AntalBørn { get; set; }
        [DataType(DataType.Date)]
        public DateTime TjekIndDato { get; set; }
        [DataType(DataType.Date)]
        public DateTime TjekUdDato { get; set; }
        public decimal TotalPris { get; set; }
        public int VærelseTypeId { get; set; }
        public int StatusId { get; set; }
        public int KundeId { get; set; }

        public virtual Kunde Kunde { get; set; }
        public virtual BookingStatus Status { get; set; }
        public virtual VærelseType VærelseType { get; set; }
    }
}




