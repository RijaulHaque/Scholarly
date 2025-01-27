//namespace Scholarly.Models
//{
//    public class Attendance
//    {
//        public int Id { get; set; }
//        public DateTime Date { get; set; }
//        public int? EnrollmentsId { get; set; }
//        public Enrollments? Enrollments { get; set; } // Navigation property foreign key

//        public bool AttendanceData { get; set; }
//    }
//}

//25th Jan code
using System;
using System.ComponentModel.DataAnnotations;
using Scholarly.Models;

namespace Scholarly.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } // Represents the first day of the month
        public int? EnrollmentsId { get; set; }
        public Enrollments? Enrollments { get; set; } // Navigation property foreign key

        // Bitmask to store attendance for multiple days in a month
        public int AttendanceData { get; set; }

        // Status to track if the attendance is approved by the teacher
        public bool IsApproved { get; set; }

        // Status to track if the attendance is submitted by the student
        public bool IsSubmitted { get; set; }
    }
}

