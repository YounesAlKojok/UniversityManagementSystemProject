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
    public class DeansController : Controller
    {
        private readonly UniversityManagementSysDbContext _context;

        public DeansController(UniversityManagementSysDbContext context)
        {
            _context = context;
        }

        // GET: Deans
        public async Task<IActionResult> Index()
        {
              return _context.Deans != null ? 
                          View(await _context.Deans.ToListAsync()) :
                          Problem("Entity set 'UniversityManagementSysDbContext.Deans'  is null.");
        }

        // GET: Deans/Details/5
        [Authorize(Roles = "Administrator,Dean")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Deans == null)
            {
                return NotFound();
            }

            var dean = await _context.Deans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dean == null)
            {
                return NotFound();
            }

            return View(dean);
        }

        // GET: Deans/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fname,Lname,Dob,Mail,Phone,Address,Office")] Dean dean)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dean);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dean);
        }

        // GET: Deans/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Deans == null)
            {
                return NotFound();
            }

            var dean = await _context.Deans.FindAsync(id);
            if (dean == null)
            {
                return NotFound();
            }
            return View(dean);
        }

        // POST: Deans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fname,Lname,Dob,Mail,Phone,Address,Office")] Dean dean)
        {
            if (id != dean.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dean);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeanExists(dean.Id))
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
            return View(dean);
        }

        // GET: Deans/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Deans == null)
            {
                return NotFound();
            }

            var dean = await _context.Deans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dean == null)
            {
                return NotFound();
            }

            return View(dean);
        }

        // POST: Deans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Deans == null)
            {
                return Problem("Entity set 'UniversityManagementSysDbContext.Deans'  is null.");
            }
            var dean = await _context.Deans.FindAsync(id);
            if (dean != null)
            {
                _context.Deans.Remove(dean);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeanExists(int id)
        {
          return (_context.Deans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
