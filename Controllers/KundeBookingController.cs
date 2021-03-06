using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Staycation.Data;
using Staycation.Models;
using Staycation.Services;

namespace Staycation.Controllers
{
    public class KundeBookingController : Controller
    {
        private readonly StaycationDBContext _context;

        KundeBookingService _kundeBookingService;

        public KundeBookingController(StaycationDBContext context, KundeBookingService kundeBookingService)
        {
            Console.WriteLine("CALLING KundeBookingController");
            _context = context;
            _kundeBookingService = kundeBookingService;
        }

        // GET: KundeBooking
        public async Task<IActionResult> Index()
        {
            var staycationDBContext = _context.Booking.Include(b => b.Kunde).Include(b => b.Status).Include(b => b.VærelseType);
            return View(await staycationDBContext.ToListAsync());
        }

        // GET: KundeBooking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Kunde)
                .Include(b => b.Status)
                .Include(b => b.VærelseType)
                .FirstOrDefaultAsync(m => m.BookingNummer == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        public IActionResult Overblik()
        {
            Booking booking = _kundeBookingService.getFinalBooking(); //Sørger for at regne total pris ud, og returnere derefter currentBooking

            return View(booking); //return view der passer til metoden
        }

        
        public IActionResult ConfirmBooking(string submit)
        {
            if (submit.Equals("Bekræft"))
            {
                Booking booking = _kundeBookingService.getCurrentBooking();
                booking.Status = null;
                booking.VærelseType = null;

                _context.Add(booking);

                List<Booking> temp = _context.Booking.ToList();

                _context.SaveChanges();

                return RedirectToAction("BookingBekræftelse");
            } else
            {
                return RedirectToAction("OpretKundeEfterBooking", "Kunde");
            }

        }

        public IActionResult BookingBekræftelse()
        {
            Booking booking = _kundeBookingService.getCurrentBooking(); //get current booking

            _kundeBookingService.setCurrentBooking(null); //resets current booking

            if (booking != null)
            {
                return View(booking);
            } else
            {
                return RedirectToAction("Create");
            }
        }

        // GET: KundeBooking/Create
        public IActionResult Create()
        {
            ViewData["VærelseTypeId"] = new SelectList(_context.VærelseType, "Id", "Beskrivelse"); // Henter værdier til dropdown listen for værelsestype

            Booking booking = _kundeBookingService.getCurrentBooking();
            
            if(booking != null) //hvis booking har en værdi, giver vi booking objektet med videre til vores view
            {
                return View(booking);
            }

            return View(new Booking()
            {
                TotalPris = 0,
                VærelseTypeId = 1,
                AntalBørn = 0,
                AntalVoksne = 0,
                TjekIndDato = DateTime.Now,
                TjekUdDato = DateTime.Now.AddDays(2)
            }); //Hvis ikke booking har en værdi, så laver vi et nyt object med nogle standard værdier, som vi giver videre til vores view
        }


        // POST: KundeBooking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingNummer,AntalVoksne,AntalBørn,TjekIndDato,TjekUdDato,TotalPris,VærelseTypeId,StatusId,KundeId")] Booking booking)
        {
            booking.StatusId = 1;
            booking.Status = await _context.BookingStatus.FirstOrDefaultAsync(bs => bs.Id == booking.StatusId); // Sets default value
            booking.VærelseType = await _context.VærelseType.FirstOrDefaultAsync(vt => vt.Id == booking.VærelseTypeId);

            booking.Kunde = _kundeBookingService.getCurrentBookingKunde();

            _kundeBookingService.setCurrentBooking(booking);
            Console.WriteLine("Booking (Create): " + _kundeBookingService.getCurrentBooking());

            return RedirectToAction("OpretKundeEfterBooking", "Kunde");
        }

        // GET: KundeBooking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["KundeId"] = new SelectList(_context.Kunde, "Id", "Efternavn", booking.KundeId);
            ViewData["StatusId"] = new SelectList(_context.BookingStatus, "Id", "Status", booking.StatusId);
            ViewData["VærelseTypeId"] = new SelectList(_context.VærelseType, "Id", "Beskrivelse", booking.VærelseTypeId);
            return View(booking);
        }

        // POST: KundeBooking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingNummer,AntalVoksne,AntalBørn,TjekIndDato,TjekUdDato,TotalPris,VærelseTypeId,StatusId,KundeId")] Booking booking)
        {
            if (id != booking.BookingNummer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingNummer))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KundeId"] = new SelectList(_context.Kunde, "Id", "Efternavn", booking.KundeId);
            ViewData["StatusId"] = new SelectList(_context.BookingStatus, "Id", "Status", booking.StatusId);
            ViewData["VærelseTypeId"] = new SelectList(_context.VærelseType, "Id", "Beskrivelse", booking.VærelseTypeId);
            return View(booking);
        }

        // GET: KundeBooking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Kunde)
                .Include(b => b.Status)
                .Include(b => b.VærelseType)
                .FirstOrDefaultAsync(m => m.BookingNummer == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: KundeBooking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.BookingNummer == id);
        }
    }
}
