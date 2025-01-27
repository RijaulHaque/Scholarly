//#################### 22nd Jan code ############################
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

//        public async Task<IActionResult> Index()
//        {
//            var user = await _context.user
//                .Where(u => u.Username == User.Identity.Name)
//                .FirstOrDefaultAsync();

//            if (user == null)
//            {
//                return NotFound("User not found.");
//            }

//            var student = await _context.student
//                .Where(s => s.Id == user.Id)
//                .Include(s => s.Enrollments)
//                .ThenInclude(e => e.Courses)
//                .FirstOrDefaultAsync();

//            if (student == null)
//            {
//                return NotFound("Student profile not found.");
//            }

//            var model = new StudentDashboardViewModel
//            {
//                StudentId = student.Id,
//                Username = user.Username,
//                Email = user.Email,
//                FullName = student.FullName ?? "N/A",
//                CurrentSemester = student.CurrentSemester,
//                Year = student.Year,
//                RegistrationNo = student.RegistrationNo ?? "N/A",
//                PhoneNo = student.PhoneNo,
//                Address = student.Address ?? "N/A",
//                Enrollments = student.Enrollments
//            };

//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> UpdateProfile(StudentDashboardViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var student = await _context.student
//                    .Where(s => s.Id == model.StudentId)
//                    .FirstOrDefaultAsync();

//                if (student == null)
//                {
//                    return NotFound("Student profile not found.");
//                }

//                // Only update the fields that are provided (excluding Username and Email)
//                student.FullName = !string.IsNullOrEmpty(model.FullName) ? model.FullName : student.FullName;
//                student.CurrentSemester = model.CurrentSemester.HasValue ? model.CurrentSemester.Value : student.CurrentSemester;
//                student.RegistrationNo = !string.IsNullOrEmpty(model.RegistrationNo) ? model.RegistrationNo : student.RegistrationNo;
//                student.PhoneNo = !string.IsNullOrEmpty(model.PhoneNo) ? model.PhoneNo : student.PhoneNo;
//                student.Address = !string.IsNullOrEmpty(model.Address) ? model.Address : student.Address;

//                //_context.student.Update(student);
//                _context.Entry(student).State = EntityState.Modified;

//                await _context.SaveChangesAsync();

//                return RedirectToAction("Index");

//            }

//            return View("Index", model);
//        }

//    }

//}

//######################### Adding Enrollment ###########
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

//        public async Task<IActionResult> Index()
//        {
//            var user = await _context.user
//                .Where(u => u.Username == User.Identity.Name)
//                .FirstOrDefaultAsync();

//            if (user == null)
//            {
//                return NotFound("User not found.");
//            }

//            var student = await _context.student
//                .Where(s => s.UserId == user.Id)
//                .Include(s => s.Enrollments)
//                .ThenInclude(e => e.Courses)
//                .FirstOrDefaultAsync();

//            if (student == null)
//            {
//                return NotFound("Student profile not found.");
//            }

//            var model = new StudentDashboardViewModel
//            {
//                StudentId = student.Id,
//                Username = user.Username,
//                Email = user.Email,
//                FullName = student.FullName ?? "N/A",
//                CurrentSemester = student.CurrentSemester,
//                Year = student.Year,
//                RegistrationNo = student.RegistrationNo ?? "N/A",
//                PhoneNo = student.PhoneNo,
//                Address = student.Address ?? "N/A",
//                Enrollments = student.Enrollments
//            };

//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> UpdateProfile(StudentDashboardViewModel model)
//        {

//            if (ModelState.IsValid)
//            {
//                var student = await _context.student
//                    .Where(s => s.Id == model.StudentId)
//                    .FirstOrDefaultAsync();

//                if (student == null)
//                {
//                    return NotFound("Student profile not found.");
//                }

//                // Only update the fields that are provided (excluding Username and Email)
//                student.FullName = !string.IsNullOrEmpty(model.FullName) ? model.FullName : student.FullName;
//                student.CurrentSemester = model.CurrentSemester.HasValue ? model.CurrentSemester.Value : student.CurrentSemester;
//                student.RegistrationNo = !string.IsNullOrEmpty(model.RegistrationNo) ? model.RegistrationNo : student.RegistrationNo;
//                student.PhoneNo = !string.IsNullOrEmpty(model.PhoneNo) ? model.PhoneNo : student.PhoneNo;
//                student.Address = !string.IsNullOrEmpty(model.Address) ? model.Address : student.Address;

//                _context.Entry(student).State = EntityState.Modified;

//                await _context.SaveChangesAsync();

//                return RedirectToAction("Index");
//            }

//            return View("Index", model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> ApplyForEnrollment(int studentId)
//        {
//            var student = await _context.student
//                .Where(s => s.Id == studentId)
//                .FirstOrDefaultAsync();

//            if (student == null)
//            {
//                return NotFound("Student not found.");
//            }

//            // Get all courses for the student's current semester
//            var semesterCourses = await _context.course
//                .Where(c => c.Semester == student.CurrentSemester)
//                .ToListAsync();

//            if (semesterCourses.Any())
//            {
//                foreach (var course in semesterCourses)
//                {
//                    // Check if the student is already enrolled in the course
//                    var existingEnrollment = await _context.Enrollments
//                        .Where(e => e.StudentId == student.Id && e.CoursesId == course.Id)
//                        .FirstOrDefaultAsync();

//                    if (existingEnrollment == null)
//                    {
//                        // Add a new enrollment record
//                        var newEnrollment = new Enrollments
//                        {
//                            StudentId = student.Id,
//                            CoursesId = course.Id,
//                            Year = student.Year,
//                            TeachersId = course.TeachersId
//                        };

//                        await _context.Enrollments.AddAsync(newEnrollment);
//                    }
//                    else
//                    {
//                        // Update the existing enrollment record
//                        existingEnrollment.Year = student.Year;
//                        existingEnrollment.TeachersId = course.TeachersId;
//                        _context.Entry(existingEnrollment).State = EntityState.Modified;
//                    }
//                }

//                await _context.SaveChangesAsync();
//            }

//            return RedirectToAction("Index");
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]

//        

//    }
//}

//25th jan
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
    public class DashboardController : Controller
    {
        private readonly DatabaseContext _context;

        public DashboardController(DatabaseContext context)
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

            var student = await _context.student
                .Where(s => s.UserId == user.Id)
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Courses)
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Attendance)
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound("Student profile not found.");
            }

            var model = new StudentDashboardViewModel
            {
                StudentId = student.Id,
                Username = user.Username,
                Email = user.Email,
                FullName = student.FullName ?? "N/A",
                CurrentSemester = student.CurrentSemester,
                Year = student.Year,
                RegistrationNo = student.RegistrationNo ?? "N/A",
                PhoneNo = student.PhoneNo,
                Address = student.Address ?? "N/A",
                Enrollments = student.Enrollments
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(StudentDashboardViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = await _context.student
                    .Where(s => s.Id == model.StudentId)
                    .FirstOrDefaultAsync();

                if (student == null)
                {
                    return NotFound("Student profile not found.");
                }

                // Only update the fields that are provided (excluding Username and Email)
                student.FullName = !string.IsNullOrEmpty(model.FullName) ? model.FullName : student.FullName;
                student.CurrentSemester = model.CurrentSemester.HasValue ? model.CurrentSemester.Value : student.CurrentSemester;
                student.RegistrationNo = !string.IsNullOrEmpty(model.RegistrationNo) ? model.RegistrationNo : student.RegistrationNo;
                student.PhoneNo = !string.IsNullOrEmpty(model.PhoneNo) ? model.PhoneNo : student.PhoneNo;
                student.Address = !string.IsNullOrEmpty(model.Address) ? model.Address : student.Address;

                _context.Entry(student).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyForEnrollment(int studentId)
        {
            var student = await _context.student
                .Where(s => s.Id == studentId)
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            // Get all courses for the student's current semester
            var semesterCourses = await _context.course
                .Where(c => c.Semester == student.CurrentSemester)
                .ToListAsync();

            if (semesterCourses.Any())
            {
                foreach (var course in semesterCourses)
                {
                    // Check if the student is already enrolled in the course
                    var existingEnrollment = await _context.Enrollments
                        .Where(e => e.StudentId == student.Id && e.CoursesId == course.Id)
                        .FirstOrDefaultAsync();

                    if (existingEnrollment == null)
                    {
                        // Add a new enrollment record
                        var newEnrollment = new Enrollments
                        {
                            StudentId = student.Id,
                            CoursesId = course.Id,
                            Year = student.Year,
                            TeachersId = course.TeachersId
                        };

                        await _context.Enrollments.AddAsync(newEnrollment);
                    }
                    else
                    {
                        // Update the existing enrollment record
                        existingEnrollment.Year = student.Year;
                        existingEnrollment.TeachersId = course.TeachersId;
                        _context.Entry(existingEnrollment).State = EntityState.Modified;
                    }
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitAttendance(int enrollmentId, bool isPresent)
        {
            var enrollment = await _context.Enrollments
                .Where(e => e.Id == enrollmentId)
                .Include(e => e.Attendance)
                .FirstOrDefaultAsync();

            if (enrollment == null)
            {
                return NotFound("Enrollment not found.");
            }

            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, today.Day);
            var attendance = enrollment.Attendance.FirstOrDefault(a => a.Date == currentMonth);

            if (attendance == null)
            {
                attendance = new Attendance
                {
                    Date = currentMonth,
                    EnrollmentsId = enrollmentId,
                    AttendanceData = 0,
                    IsSubmitted = true,
                    IsApproved = false
                };

                await _context.Attendance.AddAsync(attendance);
                await _context.SaveChangesAsync(); // Save changes to generate the Id
            }

            // Set the attendance for the specific day
            int day = today.Day - 1; // Days are 1-based, so subtract 1 for 0-based index
            if (isPresent)
            {
                attendance.AttendanceData |= (1 << day); // Set the bit for the specific day
            }
            else
            {
                attendance.AttendanceData &= ~(1 << day); // Clear the bit for the specific day
            }

            attendance.IsSubmitted = true;
            _context.Entry(attendance).State = EntityState.Modified;

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













