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
    public class StudentsController : Controller
    {
        private readonly RoommateDbContext _context;

        public StudentsController(RoommateDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var roommateDbContext = _context.Students.Include(s => s.Dormitories);
            return View(await roommateDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Include(s => s.Dormitories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        public IActionResult Create()
        {
            ViewData["DormitoriesId"] = new SelectList(_context.Dormitories.Where(x => x.MaxCapacity > x.StudentsList.Count), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Students students)
        {
            if (ModelState.IsValid)
            {
                _context.Add(students);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DormitoriesId"] = new SelectList(_context.Dormitories, "Id", "Name", students.DormitoriesId);
            return View(students);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }
            ViewData["DormitoriesId"] = new SelectList(_context.Dormitories.Where(x => x.MaxCapacity > x.StudentsList.Count), "Id", "Name", students.DormitoriesId);
            return View(students);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Students students)
        {
            if (id != students.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(students);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsExists(students.Id))
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
            ViewData["DormitoriesId"] = new SelectList(_context.Dormitories, "Name", "Id", students.DormitoriesId);
            return View(students);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Include(s => s.Dormitories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var students = await _context.Students.FindAsync(id);
            if (students != null)
            {
                _context.Students.Remove(students);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
