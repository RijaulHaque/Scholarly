using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Scholarly.Models;

namespace Scholarly.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        // Add Student Enrollment
        [HttpPost]
        public IActionResult AddEnrollment(int studentId, int courseId)
        {
            // Logic to add enrollment
            // Example: Save to database
            return Ok("Enrollment added successfully");
        }

        // Add Student Attendance
        [HttpPost]
        public IActionResult AddAttendance(int studentId, int courseId, bool isPresent)
        {
            // Logic to add attendance
            // Example: Save attendance record
            return Ok("Attendance recorded successfully");
        }

        // Add Teacher Grade
        [HttpPost]
        public IActionResult AddGrade(int studentId, int courseId, string grade)
        {
            // Logic to add grade
            // Example: Save grade record
            return Ok("Grade added successfully");
        }

        // Add Teacher Attendance
        [HttpPost]
        public IActionResult AddTeacherAttendance(int teacherId, int courseId, bool isPresent)
        {
            // Logic to add teacher attendance
            // Example: Save teacher attendance record
            return Ok("Teacher attendance recorded successfully");
        }

    }
}
