using System;
using System.Collections.Generic;
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
    [Authorize]
    public class AppoinmentsController : Controller
    {
        private readonly UniversityManagementSysDbContext _context;

        public AppoinmentsController(UniversityManagementSysDbContext context)
        {
            _context = context;
        }

        // GET: Appoinments
        public async Task<IActionResult> Index()
        {
            var universityManagementSysDbContext = _context.Appoinments.Include(a => a.Dean).Include(a => a.Student);
            return View(await universityManagementSysDbContext.ToListAsync());
        }

        // GET: Appoinments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appoinments == null)
            {
                return NotFound();
            }

            var appoinment = await _context.Appoinments
                .Include(a => a.Dean)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appoinment == null)
            {
                return NotFound();
            }

            return View(appoinment);
        }

        // GET: Appoinments/Create
        [Authorize(Roles = "Administrator,Dean,Student")]
        public IActionResult Create()
        {
            // Old
            //ViewData["DeanId"] = new SelectList(_context.Deans, "Id","FullName" ,"Mail");
            //ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName");
            //return View();

            // New
            DateTime now = DateTime.Now;
            DateTime today = new DateTime(now.Year, now.Month, now.Day);

            // Filter available slots based on the requirements
            var availableSlots = new List<DateTime>();
            for (int i = 0; i < 4; i++)
            {
                DateTime slot = today.AddHours(12).AddMinutes(i * 15);
                if (slot.DayOfWeek >= DayOfWeek.Monday && slot.DayOfWeek <= DayOfWeek.Thursday)
                {
                    availableSlots.Add(slot);
                }
            }

            ViewData["DeanId"] = new SelectList(_context.Deans, "Id", "FullName", "Mail");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName");
            ViewData["AvailableSlots"] = availableSlots;

            return View();
        }

        // POST: Appoinments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,DeanId,Duration,Reason,StudentId,IsSlotActive")] Appoinment appoinment)
        {
            if (ModelState.IsValid)
            {
                appoinment.Duration = 15; //N
                _context.Add(appoinment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeanId"] = new SelectList(_context.Deans, "Id", "Mail", appoinment.DeanId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", appoinment.StudentId);
            return View(appoinment);
        }

        // GET: Appoinments/Edit/5
        [Authorize(Roles = "Administrator,Dean")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appoinments == null)
            {
                return NotFound();
            }

            var appoinment = await _context.Appoinments.FindAsync(id);
            if (appoinment == null)
            {
                return NotFound();
            }
            ViewData["DeanId"] = new SelectList(_context.Deans, "Id","FullName" , appoinment.DeanId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", appoinment.StudentId);
            return View(appoinment);
        }

        // POST: Appoinments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,DeanId,Duration,Reason,StudentId,IsSlotActive")] Appoinment appoinment)
        {
            if (id != appoinment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appoinment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppoinmentExists(appoinment.Id))
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
            ViewData["DeanId"] = new SelectList(_context.Deans, "Id", "Mail", appoinment.DeanId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", appoinment.StudentId);
            return View(appoinment);
        }

        // GET: Appoinments/Delete/5
        [Authorize(Roles = "Administrator,Dean")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appoinments == null)
            {
                return NotFound();
            }

            var appoinment = await _context.Appoinments
                .Include(a => a.Dean)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appoinment == null)
            {
                return NotFound();
            }

            return View(appoinment);
        }

        // POST: Appoinments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appoinments == null)
            {
                return Problem("Entity set 'UniversityManagementSysDbContext.Appoinments'  is null.");
            }
            var appoinment = await _context.Appoinments.FindAsync(id);
            if (appoinment != null)
            {
                _context.Appoinments.Remove(appoinment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppoinmentExists(int id)
        {
          return (_context.Appoinments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
