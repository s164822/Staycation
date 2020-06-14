using System;
using System.Collections.Generic;

namespace Staycation.Models
{
    public partial class Fuldbooking
    {
        public int BookingNummer { get; set; }
        public int AntalVoksne { get; set; }
        public int? AntalBørn { get; set; }
        public DateTime TjekIndDato { get; set; }
        public DateTime TjekUdDato { get; set; }
        public decimal TotalPris { get; set; }
        public int VærelseTypeId { get; set; }
        public int StatusId { get; set; }
        public int KundeId { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public int AdresseId { get; set; }
        public int TelefonNummer { get; set; }
        public string Pasnummer { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public DateTime Fødselsdagsdato { get; set; }
    }
}
