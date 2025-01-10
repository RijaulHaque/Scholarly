using System.ComponentModel.DataAnnotations;

namespace Scholarly.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Enrollments> Enrollments { get; set; }
    }
}