using System;
using System.Collections.Generic;

namespace Staycation.Models
{
    public partial class Adresse
    {
        public Adresse()
        {
            Kunde = new HashSet<Kunde>();
            //Hash er fordi man giver den en anden værdi en hvad det rent faktisk er, så hvis man bliver hachek er der noget sikekrhed i, at hackeren ikke kan se hvad den sande værdi er 
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
