//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Scholarly.DAL;
//using Scholarly.Models;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Scholarly.Controllers
//{
//    public class TeacherDashboardController : Controller
//    {
//        private readonly DatabaseContext _context;

//        public TeacherDashboardController(DatabaseContext context)
//        {
//            _context = context;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var user = await _context.user
//                .Where(u => u.Username == User.Identity.Name)
//                .FirstOrDefaultAsync();

//            if (user == null)
//            {
//                return NotFound("User not found.");
//            }

//            var teacher = await _context.Teacher
//                .Where(t => t.UserId == user.Id)
//                .Include(t => t.Courses)
//                .FirstOrDefaultAsync();

//            if (teacher == null)
//            {
//                return NotFound("Teacher profile not found.");
//            }

//            var model = new TeacherDashboardViewModel
//            {
//                TeacherId = teacher.Id,
//                Username = user.Username,
//                EmployeeRegistrationNo = teacher.EmployeeRegistrationNo ?? "N/A",
//                FullName = teacher.FullName ?? "N/A",
//                PhoneNo = teacher.PhoneNo,
//                Address = teacher.Address ?? "N/A",
//                Courses = teacher.Courses.ToList()
//            };

//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> UpdateProfile(TeacherDashboardViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var teacher = await _context.Teacher
//                    .Where(t => t.Id == model.TeacherId)
//                    .FirstOrDefaultAsync();

//                if (teacher == null)
//                {
//                    return NotFound("Teacher profile not found.");
//                }

//                // Only update the fields that are provided (excluding Username)
//                teacher.FullName = !string.IsNullOrEmpty(model.FullName) ? model.FullName : teacher.FullName;
//                teacher.EmployeeRegistrationNo = !string.IsNullOrEmpty(model.EmployeeRegistrationNo) ? model.EmployeeRegistrationNo : teacher.EmployeeRegistrationNo;
//                teacher.PhoneNo = !string.IsNullOrEmpty(model.PhoneNo) ? model.PhoneNo : teacher.PhoneNo;
//                teacher.Address = !string.IsNullOrEmpty(model.Address) ? model.Address : teacher.Address;

//                _context.Entry(teacher).State = EntityState.Modified;

//                await _context.SaveChangesAsync();

//                return RedirectToAction("Index");
//            }

//            return View("Index", model);
//        }
//    }
//}

/// 25th jan code 
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scholarly.DAL;
using Scholarly.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Scholarly.Controllers
{
    public class TeacherDashboardController : Controller
    {
        private readonly DatabaseContext _context;

        public TeacherDashboardController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _context.user
                .Where(u => u.Username == User.Identity.Name)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var teacher = await _context.Teacher
                .Where(t => t.UserId == user.Id)
                .Include(t => t.Courses)
                .ThenInclude(c => c.Enrollments)
                .ThenInclude(e => e.Attendance)
                .FirstOrDefaultAsync();

            if (teacher == null)
            {
                return NotFound("Teacher profile not found.");
            }

            var model = new TeacherDashboardViewModel
            {
                TeacherId = teacher.Id,
                Username = user.Username,
                EmployeeRegistrationNo = teacher.EmployeeRegistrationNo ?? "N/A",
                FullName = teacher.FullName ?? "N/A",
                PhoneNo = teacher.PhoneNo,
                Address = teacher.Address ?? "N/A",
                Courses = teacher.Courses.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(TeacherDashboardViewModel model)
        {
            if (ModelState.IsValid)
            {
                var teacher = await _context.Teacher
                    .Where(t => t.Id == model.TeacherId)
                    .FirstOrDefaultAsync();

                if (teacher == null)
                {
                    return NotFound("Teacher profile not found.");
                }

                // Only update the fields that are provided (excluding Username)
                teacher.FullName = !string.IsNullOrEmpty(model.FullName) ? model.FullName : teacher.FullName;
                teacher.EmployeeRegistrationNo = !string.IsNullOrEmpty(model.EmployeeRegistrationNo) ? model.EmployeeRegistrationNo : teacher.EmployeeRegistrationNo;
                teacher.PhoneNo = !string.IsNullOrEmpty(model.PhoneNo) ? model.PhoneNo : teacher.PhoneNo;
                teacher.Address = !string.IsNullOrEmpty(model.Address) ? model.Address : teacher.Address;

                _context.Entry(teacher).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveAttendance(int courseId, DateTime date)
        {
            var course = await _context.course
                .Where(c => c.Id == courseId)
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Attendance)
                .FirstOrDefaultAsync();

            if (course == null)
            {
                return NotFound("Course not found.");
            }

            int day = date.Day - 1; // Days are 1-based, so subtract 1 for 0-based index

            foreach (var enrollment in course.Enrollments)
            {
                var attendance = enrollment.Attendance.FirstOrDefault(a => a.Date.Year == date.Year && a.Date.Month == date.Month);
                if (attendance != null)
                {
                    // Update the AttendanceData to mark the student as present for the specific day
                    attendance.AttendanceData |= (1 << day); // Set the bit for the specific day
                    attendance.IsApproved = true;

                    _context.Entry(attendance).State = EntityState.Modified;
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}



