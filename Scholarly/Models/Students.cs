namespace Scholarly.Models
{
    public class Students
    {
        public string studentId { get; set; } = string.Empty;
        public int date_of_birth { get; set; }
        public string gender { get; set; } = string.Empty;
        public int phone_number { get; set; }
        public string address { get; set; } = string.Empty;
        public ICollection<Attendance> atttendance_obj { get; set; } = new List<Attendance>();
        public ICollection<Enrollments> enrollments_obj { get; set; } = new List<Enrollments>();
        public ICollection<Grades> grades_obj { get; set; } = new List<Grades>();
    }
}
