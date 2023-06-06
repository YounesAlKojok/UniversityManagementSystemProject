using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Data;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{

    public class FacultiesController : Controller
    {
        private readonly UniversityManagementSysDbContext _context;

        public FacultiesController(UniversityManagementSysDbContext context)
        {
            _context = context;
        }

        // GET: Faculties
        public async Task<IActionResult> Index()
        {
            // 1 var universityManagementSysDbContext = _context.Faculties.Include(f => f.Dean).Include(f => f.Departments).ToListAsync(); ;
            // 1 return View(await universityManagementSysDbContext.ToListAsync());
            //2 return View(universityManagementSysDbContext);
            var faculties = await _context.Faculties
         .Include(f => f.Dean)
         .Include(f => f.Departments)
         .ToListAsync();

            return View(faculties);
        }

        // GET: Faculties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Faculties == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties
                .Include(f => f.Dean)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // GET: Faculties/Create
        [Authorize(Roles = "Administrator,Dean")]
        public IActionResult Create()
        {
            ViewData["DeanId"] = new SelectList(_context.Deans, "Id", "FullName", "Mail");
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DeanId,Location,Name")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faculty);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Faculty record was created";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeanId"] = new SelectList(_context.Deans, "Id", "Mail", faculty.DeanId);
            return View(faculty);
        }

        // GET: Faculties/Edit/5
        [Authorize(Roles = "Administrator,Dean")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Faculties == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }
            ViewData["DeanId"] = new SelectList(_context.Deans, "Id", "Mail", faculty.DeanId);
            return View(faculty);
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DeanId,Location,Name")] Faculty faculty)
        {
            if (id != faculty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faculty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyExists(faculty.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Edit"] = "Faculty record was Modified";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeanId"] = new SelectList(_context.Deans, "Id", "Mail", faculty.DeanId);
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        [Authorize(Roles = "Administrator,Dean")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Faculties == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties
                .Include(f => f.Dean)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Faculties == null)
            {
                return Problem("Entity set 'UniversityManagementSysDbContext.Faculties'  is null.");
            }
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty != null)
            {
                _context.Faculties.Remove(faculty);
            }
            
            await _context.SaveChangesAsync();
            TempData["Edit"] = "Faculty record was Deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool FacultyExists(int id)
        {
          return (_context.Faculties?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
