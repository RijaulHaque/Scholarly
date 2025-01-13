using Microsoft.AspNetCore.Mvc;
using Scholarly.DAL;
using Scholarly.Models;

namespace Scholarly.Controllers
{
    public class AdminController : Controller
    {
        private readonly DatabaseContext _context;

        public AdminController(DatabaseContext context)
        {
            _context = context;
        }

        // Add Course
        [HttpPost]
        public IActionResult AddCourse(string courseName)
        {
            var course = new Courses
            {
                Name = courseName,
                Enrollments = new List<Enrollments>()
            };

            _context.course.Add(course);
            _context.SaveChanges();

            return Ok("Course added successfully.");
        }
    }
}