using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppDemoStudent.Data;
using WebAppDemoStudent.Models;

namespace WebAppDemoStudent.Controllers
{
    public class BookingsController : Controller
    {
        private readonly MyDbContext _context;

        public BookingsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            return _context.Bookings != null ?
                        View(await _context.Bookings.ToListAsync()) :
                        Problem("Entity set 'MyDbContext.Bookings'  is null.");
        }

        //Ger user by email

        [HttpGet]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {

                ModelState.AddModelError("Email", "Email is required.");

                return View("Index");
            }

            var bookings = await _context.Bookings
                .Where(b => b.Email == email)
                .ToListAsync();

            if (bookings.Count == 0)
            {
                // Redirect to the "Create" view when there are no bookings
                return RedirectToAction("Create");
            }

            // Redirect to the "GetUserByEmail" view and pass the bookings data
            return View("GetUserByEmail", bookings);
        }



        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Id,Date,Weight,Height,Symptoms,Doctor")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();

                if (User.IsInRole("patient"))
                {
                    // Redirect patients to the "GetUserByEmail" view
                    return RedirectToAction(nameof(GetUserByEmail), new { email = booking.Email });
                }

                // Redirect doctors to the "Index" view
                return RedirectToAction(nameof(Index));
            }

            return View(booking);
        }


        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Email,Id,Date,Weight,Height,Symptoms,Doctor")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (User.IsInRole("patient") || User.IsInRole("doctor"))
                {
                    // Redirect patients to the "GetUserByEmail" view
                    return RedirectToAction(nameof(GetUserByEmail), new { email = booking.Email });
                }

                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bookings == null)
            {
                return Problem("Entity set 'MyDbContext.Bookings'  is null.");
            }
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }
            
            await _context.SaveChangesAsync();
            if (User.IsInRole("patient") || User.IsInRole("doctor"))
            {
                // Redirect patients to the "GetUserByEmail" view
                return RedirectToAction(nameof(GetUserByEmail), new { email = booking.Email });
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointmentsByDoctor(string doctorName)
        {
            
            if (string.IsNullOrEmpty(doctorName))
            {
                ModelState.AddModelError("DoctorName", "Doctor name is required.");
                return View("Index");
            }

            var bookings = await _context.Bookings
                .Where(b => b.Doctor == doctorName)
                .ToListAsync();

            return View("GetUserByEmail", bookings);
        }

        private bool BookingExists(int id)
        {
          return (_context.Bookings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
