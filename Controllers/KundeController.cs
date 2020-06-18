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
    public class KundeController : Controller
    {
        private readonly StaycationDBContext _context;
        KundeBookingService _kundeBookingService;

        public KundeController(StaycationDBContext context, KundeBookingService kundeBookingService)
        {
            _context = context;
            _kundeBookingService = kundeBookingService;
        }

        // GET: Kunde
        public async Task<IActionResult> Index()
        {
            var staycationDBContext = _context.Kunde.Include(k => k.Adresse);
            return View(await staycationDBContext.ToListAsync());
        }

        // GET: Kunde/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kunde = await _context.Kunde
                .Include(k => k.Adresse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kunde == null)
            {
                return NotFound();
            }

            return View(kunde);
        }

        // GET: Kunde/OpretKundeEfterBooking
        public IActionResult OpretKundeEfterBooking()
        {
            return View();
        }


        // POST: Kunde/OpretKundeEfterBooking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KundeOgBookingOverblik(Kunde kunde)
        {
            _kundeBookingService.setCurrentBookingKunde(kunde);
  
            return RedirectToAction("Overblik", "KundeBooking");
        }


        // GET: Kunde/Create
        public IActionResult Create()
        {
            ViewData["AdresseId"] = new SelectList(_context.Adresse, "Id", "Adresse1");
            return View();
        }

        // POST: Kunde/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,AdresseId,TelefonNummer,Pasnummer,Fornavn,Efternavn,Fødselsdagsdato")] Kunde kunde)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kunde);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresseId"] = new SelectList(_context.Adresse, "Id", "Adresse1", kunde.AdresseId);
            return View(kunde);
        }

        // GET: Kunde/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kunde = await _context.Kunde.FindAsync(id);
            if (kunde == null)
            {
                return NotFound();
            }
            ViewData["AdresseId"] = new SelectList(_context.Adresse, "Id", "Adresse1", kunde.AdresseId);
            return View(kunde);
        }

        // POST: Kunde/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,AdresseId,TelefonNummer,Pasnummer,Fornavn,Efternavn,Fødselsdagsdato")] Kunde kunde)
        {
            if (id != kunde.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kunde);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KundeExists(kunde.Id))
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
            ViewData["AdresseId"] = new SelectList(_context.Adresse, "Id", "Adresse1", kunde.AdresseId);
            return View(kunde);
        }

        // GET: Kunde/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kunde = await _context.Kunde
                .Include(k => k.Adresse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kunde == null)
            {
                return NotFound();
            }

            return View(kunde);
        }

        // POST: Kunde/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kunde = await _context.Kunde.FindAsync(id);
            _context.Kunde.Remove(kunde);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KundeExists(int id)
        {
            return _context.Kunde.Any(e => e.Id == id);
        }
    }
}
