using System;
using System.Collections.Generic;

namespace Staycation.Models
{
    public partial class Adresse
    {
        public Adresse()
        {
            Kunde = new HashSet<Kunde>();
        }

        public int Id { get; set; }
        public string Adresse1 { get; set; }
        public string Nummer { get; set; }
        public string Etage { get; set; }
        public int PostNummer { get; set; }
        public string By { get; set; }

        public virtual ICollection<Kunde> Kunde { get; set; }
    }
}
