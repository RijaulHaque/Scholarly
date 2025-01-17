namespace Scholarly.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int EnrollmentsId { get; set; }
        public Enrollments Enrollments { get; set; } // Navigation property foreign key

        public bool AttendanceData { get; set; }
    }
}