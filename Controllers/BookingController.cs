using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Staycation.Data;
using Staycation.Models;

namespace Staycation.Controllers
{
    public class BookingController : Controller
    {
        private readonly StaycationDBContext _context;

        public BookingController(StaycationDBContext context)
        {
            _context = context;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
            var staycationDBContext = _context.Booking.Include(b => b.Kunde).Include(b => b.Status).Include(b => b.VærelseType);
            return View(await staycationDBContext.ToListAsync());
        }

        // GET: Booking/Details/5
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

        // GET: Booking/Create
        public IActionResult Create()
        {
            ViewData["KundeId"] = new SelectList(_context.Kunde, "Id", "Efternavn");
            ViewData["StatusId"] = new SelectList(_context.BookingStatus, "Id", "Status");
            ViewData["VærelseTypeId"] = new SelectList(_context.VærelseType, "Id", "Type");
            return View();
        }

        // POST: Booking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingNummer,AntalVoksne,AntalBørn,TjekIndDato,TjekUdDato,TotalPris,VærelseTypeId,StatusId,KundeId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KundeId"] = new SelectList(_context.Kunde, "Id", "Efternavn", booking.KundeId);
            ViewData["StatusId"] = new SelectList(_context.BookingStatus, "Id", "Status", booking.StatusId);
            ViewData["VærelseTypeId"] = new SelectList(_context.VærelseType, "Id", "Type", booking.VærelseTypeId);
            return View(booking);
        }

        // GET: Booking/Edit/5
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
            ViewData["VærelseTypeId"] = new SelectList(_context.VærelseType, "Id", "Type", booking.VærelseTypeId);
            return View(booking);
        }

        // POST: Booking/Edit/5
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
            ViewData["VærelseTypeId"] = new SelectList(_context.VærelseType, "Id", "Type", booking.VærelseTypeId);
            return View(booking);
        }

        // GET: Booking/Delete/5
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

        // POST: Booking/Delete/5
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
