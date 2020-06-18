using System;
using System.Collections.Generic;

namespace Staycation.Models
{
    public partial class VærelseType
    {
        public VærelseType()
        {
            Booking = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Pris { get; set; }
        public string Beskrivelse { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}
