namespace Scholarly.Models
{
    public class Attendance
    {
        //PK
        public string attendance_id { get; set; } = string.Empty;
        public DateOnly attendence_date { get; set; }

        //fk
        public Students Students_id { get; set; } = new Students();

        public Classes class_id { get; set; } = new Classes
        {
            class_id = string.Empty,
            class_name = string.Empty,
            Id = new Users
            {
                Id = string.Empty,
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty,
                Password = string.Empty,
                role_id = new Role(),
                students_obj = new List<Students>(),
                Classes_obj = new List<Classes>()
            },
            atttendance_obj = new List<Attendance>(),
            enrollments_obj = new List<Enrollments>(),
            grades_obj = new List<Grades>()
        };
    }
}