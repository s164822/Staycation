using System;
using Staycation.Models;

namespace Staycation.Services
{
    public class KundeBookingService
    {
        public Booking currentBooking;

        public KundeBookingService()
        {

        }

        public void setCurrentBookingKunde(Kunde kunde)
        {
            if(currentBooking != null)
            {
                currentBooking.Kunde = kunde;
            }
        }

        public Kunde getCurrentBookingKunde()
        {
            return currentBooking != null ? currentBooking.Kunde : null;
        }

        public void setCurrentBooking(Booking booking)
        {
            currentBooking = booking;
        }

        public Booking getCurrentBooking()
        {
            return currentBooking;
        }

        public Booking getFinalBooking()
        {
            calculateTotalPrice();

            return currentBooking;
        }

        private void calculateTotalPrice()
        {

            //LOGIK FOR PRISBEREGNING

            decimal prisPrVoksenPrNat = currentBooking.VærelseType.Pris * currentBooking.AntalVoksne;

            decimal prisPrBarnPrNat = 0;
            if (currentBooking.AntalBørn != null)
            {
                prisPrBarnPrNat = currentBooking.VærelseType.Pris * 0.5m * (int) currentBooking.AntalBørn;
            }

            decimal totalPrisPrNat = prisPrVoksenPrNat + prisPrBarnPrNat;

            decimal totalPris = totalPrisPrNat * (decimal) (currentBooking.TjekUdDato - currentBooking.TjekIndDato).TotalDays;

            currentBooking.TotalPris = totalPris;

        }







    }
}
