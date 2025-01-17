using System.ComponentModel.DataAnnotations;
using Scholarly.Models;

namespace Scholarly.Models
{
    public class Enrollments
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int Year { get; set; }
        public float Grade { get; set; }
        public int TeachersId { get; set; }
        public int CoursesId { get; set; }
        public Courses Courses { get; set; } // Navigation property foreign key
        public virtual ICollection<Attendance> Attendance { get; set; }
    }
}
