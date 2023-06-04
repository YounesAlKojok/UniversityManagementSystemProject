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
    [Authorize]
    public class DepartmentsCoursesController : Controller
    {
        private readonly UniversityManagementSysDbContext _context;

        public DepartmentsCoursesController(UniversityManagementSysDbContext context)
        {
            _context = context;
        }

        // GET: DepartmentsCourses
        public async Task<IActionResult> Index()
        {
            var universityManagementSysDbContext = _context.DepartmentsCourses.Include(d => d.Course).Include(d => d.Department);
            return View(await universityManagementSysDbContext.ToListAsync());
        }

        // GET: DepartmentsCourses/Details/5
        [Authorize(Roles = "Administrator,Dean")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DepartmentsCourses == null)
            {
                return NotFound();
            }

            var departmentsCourses = await _context.DepartmentsCourses
                .Include(d => d.Course)
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departmentsCourses == null)
            {
                return NotFound();
            }

            return View(departmentsCourses);
        }

        // GET: DepartmentsCourses/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: DepartmentsCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentId,CourseId")] DepartmentsCourses departmentsCourses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departmentsCourses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", departmentsCourses.CourseId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", departmentsCourses.DepartmentId);
            return View(departmentsCourses);
        }

        // GET: DepartmentsCourses/Edit/5
        [Authorize(Roles = "Administrator,Dean")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DepartmentsCourses == null)
            {
                return NotFound();
            }

            var departmentsCourses = await _context.DepartmentsCourses.FindAsync(id);
            if (departmentsCourses == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", departmentsCourses.CourseId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", departmentsCourses.DepartmentId);
            return View(departmentsCourses);
        }

        // POST: DepartmentsCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentId,CourseId")] DepartmentsCourses departmentsCourses)
        {
            if (id != departmentsCourses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departmentsCourses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentsCoursesExists(departmentsCourses.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", departmentsCourses.CourseId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", departmentsCourses.DepartmentId);
            return View(departmentsCourses);
        }

        // GET: DepartmentsCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DepartmentsCourses == null)
            {
                return NotFound();
            }

            var departmentsCourses = await _context.DepartmentsCourses
                .Include(d => d.Course)
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departmentsCourses == null)
            {
                return NotFound();
            }

            return View(departmentsCourses);
        }

        // POST: DepartmentsCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DepartmentsCourses == null)
            {
                return Problem("Entity set 'UniversityManagementSysDbContext.DepartmentsCourses'  is null.");
            }
            var departmentsCourses = await _context.DepartmentsCourses.FindAsync(id);
            if (departmentsCourses != null)
            {
                _context.DepartmentsCourses.Remove(departmentsCourses);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentsCoursesExists(int id)
        {
          return (_context.DepartmentsCourses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
