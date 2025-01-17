using System.Collections.Generic;

namespace Scholarly.Models
{
    public class StudentDashboardViewModel
    {
        public int StudentId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Year { get; set; }
        public List<Enrollments> Enrollments { get; set; }
    }
}
