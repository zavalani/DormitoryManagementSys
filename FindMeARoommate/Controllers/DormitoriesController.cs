using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FindMeARoommate.Models;

namespace FindMeARoommate.Controllers
{
    public class DormitoriesController : Controller
    {
        private readonly RoommateDbContext _context;

        public DormitoriesController(RoommateDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Dormitories.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormitories = await _context.Dormitories
                .FirstOrDefaultAsync(m => m.Id == id);
            dormitories.StudentsList = _context.Students.Where(x => x.DormitoriesId == dormitories.Id).ToList();
            if (dormitories == null)
            {
                return NotFound();
            }

            return View(dormitories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dormitories dormitories)
        {
            dormitories.StudentsList = _context.Students.Where(x => x.DormitoriesId == dormitories.Id).ToList();
            if (ModelState.IsValid)
            {
                _context.Add(dormitories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dormitories);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormitories = await _context.Dormitories.FindAsync(id);
            if (dormitories == null)
            {
                return NotFound();
            }
            return View(dormitories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Dormitories dormitories)
        {
            if (id != dormitories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dormitories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DormitoriesExists(dormitories.Id))
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
            return View(dormitories);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormitories = await _context.Dormitories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dormitories == null)
            {
                return NotFound();
            }

            return View(dormitories);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dormitories = await _context.Dormitories.FindAsync(id);
            if (dormitories != null)
            {
                _context.Dormitories.Remove(dormitories);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DormitoriesExists(int id)
        {
            return _context.Dormitories.Any(e => e.Id == id);
        }
    }
}
