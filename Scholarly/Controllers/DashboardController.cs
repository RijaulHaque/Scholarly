//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Scholarly.DAL;
//using Scholarly.Models;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Scholarly.Controllers
//{
//    public class DashboardController : Controller
//    {
//        private readonly DatabaseContext _context;

//        public DashboardController(DatabaseContext context)
//        {
//            _context = context;
//        }
//        //public IActionResult StudentDashboard()
//        //{
//        //    return View();
//        //}

//        public async Task<IActionResult> StudentDashboard()
//        {
//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//            var user = await _context.user
//                .Where(u => u.Username == User.Identity.Name)
//                .FirstOrDefaultAsync();
//#pragma warning restore CS8602 // Dereference of a possibly null reference.

//            if (user == null)
//            {
//                return NotFound();
//            }

//            var student = await _context.student
//                .Where(s => s.Name == user.Username)
//                .FirstOrDefaultAsync();

//            if (student == null)
//            {
//                return NotFound();
//            }

//            var enrollments = await _context.Enrollments
//                .Where(e => e.StudentId == student.Id)
//                .Include(e => e.Courses)
//                .Include(e => e.Attendance)
//                .ToListAsync();

//            var model = new StudentDashboardViewModel
//            {
//                StudentId = student.Id,
//                Username = user.Username,
//                Email = user.Email,
//                Year = student.Year,
//                Enrollments = enrollments
//            };

//            return View(model);
//        }
//    }
//}

//################################# 17th JAN ##############################
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scholarly.DAL;
using Scholarly.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Scholarly.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DatabaseContext _context;

        public DashboardController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> StudentDashboard()
        {
            // Ensure user is authenticated
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Index", "Login");
            }

            // Fetch the logged-in user
            var user = await _context.user
                .FirstOrDefaultAsync(u => u.Username == User.Identity.Name);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Fetch the corresponding student profile
            var student = await _context.student
                .FirstOrDefaultAsync(s => s.Name == user.Username);

            if (student == null)
            {
                return NotFound("Student profile not found.");
            }

            // Fetch enrollments with courses and attendance
            var enrollments = await _context.Enrollments
                .Where(e => e.StudentId == student.Id)
                .Include(e => e.Courses)
                .Include(e => e.Attendance)
                .ToListAsync();

            // Create the view model
            var model = new StudentDashboardViewModel
            {
                StudentId = student.Id,
                Username = user.Username,
                Email = user.Email,
                Year = student.Year,
                Enrollments = enrollments ?? new List<Enrollments>() // Ensure no null reference
            };

            return View(model);
        }
    }
}

