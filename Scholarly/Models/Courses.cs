using System.ComponentModel.DataAnnotations;

namespace Scholarly.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Semester { get; set; } //course belongs to the semster
        public int? TeachersId { get; set; }
        public virtual ICollection<Enrollments> Enrollments { get; set; }
    }
}