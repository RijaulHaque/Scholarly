//using System.Diagnostics;
//using Microsoft.AspNetCore.Mvc;
//using Scholarly.Models;

//namespace Scholarly.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger)
//        {
//            _logger = logger;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }



//        // Add Student Enrollment
//        [HttpPost]
//        public IActionResult AddEnrollment(int studentId, int courseId)
//        {
//            // Logic to add enrollment
//            // Example: Save to database
//            return Ok("Enrollment added successfully");
//        }

//        // Add Student Attendance
//        [HttpPost]
//        public IActionResult AddAttendance(int studentId, int courseId, bool isPresent)
//        {
//            // Logic to add attendance
//            // Example: Save attendance record
//            return Ok("Attendance recorded successfully");
//        }

//        // Add Teacher Grade
//        [HttpPost]
//        public IActionResult AddGrade(int studentId, int courseId, string grade)
//        {
//            // Logic to add grade
//            // Example: Save grade record
//            return Ok("Grade added successfully");
//        }

//        // Add Teacher Attendance
//        [HttpPost]
//        public IActionResult AddTeacherAttendance(int teacherId, int courseId, bool isPresent)
//        {
//            // Logic to add teacher attendance
//            // Example: Save teacher attendance record
//            return Ok("Teacher attendance recorded successfully");
//        }

//    }
//}

//####################### NEW CODE 16 JAN #######################
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scholarly.DAL;
using Scholarly.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Scholarly.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> StudentDashboard()
        {
            var user = await _context.user
                .Where(u => u.Username == User.Identity.Name)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            var student = await _context.student
                .Where(s => s.Name == user.Username)
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound();
            }

            var enrollments = await _context.Enrollments
                .Where(e => e.StudentId == student.Id)
                .Include(e => e.Courses)
                .Include(e => e.Attendance)
                .ToListAsync();

            var model = new StudentDashboardViewModel
            {
                StudentId = student.Id,
                Username = user.Username,
                Email = user.Email,
                Year = student.Year,
                Enrollments = enrollments
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAttendance(int enrollmentId, int studentId, bool isPresent)
        {
            var attendance = new Attendance
            {
                Date = DateTime.Now,
                EnrollmentsId = enrollmentId,
                AttendanceData = isPresent
            };

            _context.Attendance.Add(attendance);
            await _context.SaveChangesAsync();

            return RedirectToAction("StudentDashboard");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

