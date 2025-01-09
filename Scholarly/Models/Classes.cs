namespace Scholarly.Models
{
    public class Classes
    {
        //PK
        public required string class_id { get; set; }

        public required string class_name { get; set; }

        //FK
        public required Users Id { get; set; }

        public required ICollection<Attendance> atttendance_obj { get; set; } = new List<Attendance>();

        public required ICollection<Enrollments> enrollments_obj { get; set; } = new List<Enrollments>();

        public required ICollection<Grades> grades_obj { get; set; } = new List<Grades>();
    }
}