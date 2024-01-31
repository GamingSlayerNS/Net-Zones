using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Forums.Data;
using Web_Forums.Models;

namespace Web_Forums.Controllers
{
    public class ZonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zones
        public async Task<IActionResult> Index()
        {
              return _context.Zone != null ? 
                          View(await _context.Zone.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Zone'  is null.");
        }

        // GET: Zones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Zone == null)
            {
                return NotFound();
            }

            var zone = await _context.Zone
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // GET: Zones/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ZoneTopic,ZoneDiscussion,Author,Category")] Zone zone)
        {
            //// Set the Author property to the current user's name
            //zone.Author = User.Identity?.Name;

            //// Add the zone to the context and save changes
            //_context.Add(zone);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            if (ModelState.IsValid)
            {
                // Add the zone to the context and save changes
                _context.Add(zone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            System.Diagnostics.Debug.WriteLine("This will be displayed in output window");
            return View(zone);
        }

        // GET: Zones/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Zone == null)
            {
                return NotFound();
            }

            var zone = await _context.Zone.FindAsync(id);
            if (zone == null)
            {
                return NotFound();
            }
            return View(zone);
        }

        // POST: Zones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ZoneTopic,ZoneDiscussion")] Zone zone)
        {
            if (id != zone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZoneExists(zone.Id))
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
            return View(zone);
        }

        // GET: Zones/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zone == null)
            {
                return NotFound();
            }

            var zone = await _context.Zone
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // POST: Zones/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Zone == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Zone'  is null.");
            }
            var zone = await _context.Zone.FindAsync(id);
            if (zone != null)
            {
                _context.Zone.Remove(zone);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Zones/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return _context.Zone != null ?
                        View() :
                        Problem("Entity set 'ApplicationDbContext.Zone'  is null.");
        }

        // POST: Zones/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchTopic)
        {
            return _context.Zone != null ?
                        View("Index", await _context.Zone.Where( z => z.ZoneTopic.Contains(SearchTopic) ).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Zone'  is null.");
        }

        private bool ZoneExists(int id)
        {
          return (_context.Zone?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
