﻿using System;
using System.Collections.Generic;

namespace Staycation.Models
{
    public partial class Kunde
    {
        public Kunde()
        {
            Booking = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public int AdresseId { get; set; }
        public int TelefonNummer { get; set; }
        public string Pasnummer { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public DateTime Fødselsdagsdato { get; set; }

        public virtual Adresse Adresse { get; set; }
        public virtual ICollection<Booking> Booking { get; set; }
    }
}