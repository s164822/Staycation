using System;
using Staycation.Models;

namespace Staycation.Services
{
    public class KundeBookingService
    {
        private static KundeBookingService _service;

        public Booking currentBooking;

        public KundeBookingService()
        {

        }

        public static KundeBookingService getInstance()
        {
            if (_service == null)
            {
                return getInstance();
            }
            return _service;
        }

        public void setCurrentBookingKunde(Kunde kunde)
        {
            currentBooking.Kunde = kunde;
        }

        public Kunde getCurrentBookingKunde()
        {
            return currentBooking.Kunde;
        }

        public void setCurrentBooking(Booking booking)
        {
            currentBooking = booking;
        }

        public Booking getCurrentBooking()
        {
            return currentBooking;
        }







    }
}
