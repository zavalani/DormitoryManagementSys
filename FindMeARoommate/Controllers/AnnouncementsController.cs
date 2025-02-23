using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FindMeARoommate;
using FindMeARoommate.Models;

namespace FindMeARoommate.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly RoommateDbContext _context;

        public AnnouncementsController(RoommateDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Announcements.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcements = await _context.Announcements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcements == null)
            {
                return NotFound();
            }

            return View(announcements);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Announcements announcements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(announcements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(announcements);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcements = await _context.Announcements.FindAsync(id);
            if (announcements == null)
            {
                return NotFound();
            }
            return View(announcements);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Announcements announcements)
        {
            if (id != announcements.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementsExists(announcements.Id))
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
            return View(announcements);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcements = await _context.Announcements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcements == null)
            {
                return NotFound();
            }

            return View(announcements);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var announcements = await _context.Announcements.FindAsync(id);
            if (announcements != null)
            {
                announcements.IsActive = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementsExists(int id)
        {
            return _context.Announcements.Any(e => e.Id == id);
        }
    }
}
