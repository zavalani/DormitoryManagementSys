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
    public class ApplicationsController : Controller
    {
        private readonly RoommateDbContext _context;

        public ApplicationsController(RoommateDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var roommateDbContext = _context.Applications.Include(a => a.Announcements).Include(a => a.Students);
            return View(await roommateDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applications = await _context.Applications
                .Include(a => a.Announcements)
                .Include(a => a.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applications == null)
            {
                return NotFound();
            }

            return View(applications);
        }

        public IActionResult Create()
        {
            ViewData["AnnouncementsId"] = new SelectList(_context.Announcements.Where(x => x.IsActive == true), "Id", "Title");
            ViewData["StudentsId"] = new SelectList(_context.Students, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Applications applications)
        {
        
            if (ModelState.IsValid)
            {
                _context.Add(applications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnnouncementsId"] = new SelectList(_context.Announcements, "Id", "Title", applications.AnnouncementsId);
            ViewData["StudentsId"] = new SelectList(_context.Students, "Id", "Name", applications.StudentsId);
            return View(applications);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applications = await _context.Applications.FindAsync(id);
            if (applications == null)
            {
                return NotFound();
            }
            ViewData["AnnouncementsId"] = new SelectList(_context.Announcements, "Id", "Id", applications.AnnouncementsId);
            ViewData["StudentsId"] = new SelectList(_context.Students, "Id", "Id", applications.StudentsId);
            return View(applications);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Applications applications)
        {
            if (id != applications.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationsExists(applications.Id))
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
            ViewData["AnnouncementsId"] = new SelectList(_context.Announcements, "Id", "Id", applications.AnnouncementsId);
            ViewData["StudentsId"] = new SelectList(_context.Students, "Id", "Id", applications.StudentsId);
            return View(applications);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applications = await _context.Applications
                .Include(a => a.Announcements)
                .Include(a => a.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applications == null)
            {
                return NotFound();
            }

            return View(applications);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applications = await _context.Applications.FindAsync(id);
            if (applications != null)
            {
                _context.Applications.Remove(applications);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationsExists(int id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }
    }
}
