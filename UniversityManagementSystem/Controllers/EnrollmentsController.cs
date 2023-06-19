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
    public class EnrollmentsController : Controller
    {
        private readonly UniversityManagementSysDbContext _context;

        public EnrollmentsController(UniversityManagementSysDbContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var universityManagementSysDbContext = _context.Enrollments.Include(e => e.Course).Include(e => e.Student).Include(e => e.Course.Lecturer); ;
            return View(await universityManagementSysDbContext.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        [Authorize(Roles = "Administrator,Dean")]
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,EnrolDate,Semester,StudentId,Year,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                // Check if a similar enrollment already exists
                bool enrollmentExists = await _context.Enrollments.AnyAsync(e =>
                    e.CourseId == enrollment.CourseId &&
                    e.StudentId == enrollment.StudentId &&
                    e.Year == enrollment.Year &&
                    e.Semester == enrollment.Semester);

                if (enrollmentExists)
                {
                    ModelState.AddModelError("", "An enrollment with the same course, student, year, and semester already exists.");
                    ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", enrollment.CourseId);
                    ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", enrollment.StudentId);
                    return View(enrollment);
                }

                // Check if a hasTimeConflict enrollment has conflic of time/day of study conflict exist
                bool hasTimeConflict = await _context.Enrollments.AnyAsync(e =>
                   e.StudentId == enrollment.StudentId &&
                   e.Year == enrollment.Year &&
                   e.Semester == enrollment.Semester &&
                   _context.Courses.Any(c =>
                       c.Id == enrollment.CourseId &&
                       c.DayOfStudy == e.Course.DayOfStudy &&
                       c.TimeOfStudy == e.Course.TimeOfStudy));
                if (hasTimeConflict)
                {
                    ModelState.AddModelError("", "The course you are trying to enroll conflicts with the day and time of another course already enrolled by the same student.");
                    ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", enrollment.CourseId);
                    ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", enrollment.StudentId);
                    return View(enrollment);
                }
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Enrollment record was created";
                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", enrollment.StudentId);
            return View(enrollment);
        }

        // End Of New

        // GET: Enrollments/Edit/5
        [Authorize(Roles = "Administrator, Lecturer, Dean")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,EnrolDate,Semester,StudentId,Year,Grade")] Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }

            // Retrieve the logged-in Authorized email
            string authEmail = User.Identity.Name;

            // Find the enrollment and include the course's lecturer
            var enrollmentToUpdate = await _context.Enrollments
                .Include(e => e.Course)
                .ThenInclude(c => c.Lecturer)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enrollmentToUpdate == null)
            {
                return NotFound();
            }

            // Check if the logged-in lecturer's email matches the course's lecturer email
            if (User.IsInRole("Administrator") || User.IsInRole("Dean") || enrollmentToUpdate.Course.Lecturer.Mail == authEmail)
            {
                if (enrollmentToUpdate.Course.Lecturer.Mail == authEmail)
                {
                    // Update the enrollment's grade
                    enrollmentToUpdate.Grade = enrollment.Grade;
                }
                else
                {
                    // Update the enrollment's grade
                    enrollmentToUpdate.CourseId = enrollment.CourseId;
                    enrollmentToUpdate.StudentId = enrollment.StudentId;
                    enrollmentToUpdate.Semester = enrollment.Semester;
                    enrollmentToUpdate.Year = enrollment.Year;
                    enrollmentToUpdate.Grade = enrollment.Grade;


                }


                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(enrollmentToUpdate);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EnrollmentExists(enrollmentToUpdate.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    TempData["Edit"] = "Enrollment record was Modified";
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", enrollment.CourseId);
                ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", enrollment.StudentId);
                return View(enrollmentToUpdate);
            }

            // If the logged-in lecturer's email does not match, show an error message
            ModelState.AddModelError("", "You are not authorized to edit the enrollment grade.");
            return View(enrollmentToUpdate);
        }

        // GET: Enrollments/Delete/5
        [Authorize(Roles = "Administrator,Dean")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Enrollments == null)
            {
                return Problem("Entity set 'UniversityManagementSysDbContext.Enrollments'  is null.");
            }
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }
            
            await _context.SaveChangesAsync();
            TempData["Edit"] = "Enrollment record was Deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
          return (_context.Enrollments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
