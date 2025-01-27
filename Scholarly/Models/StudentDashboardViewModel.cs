////############## 21st jan for mapping StudentDeshView to students #######################

//using System.Collections.Generic;

//namespace Scholarly.Models
//{
//    public class StudentDashboardViewModel
//    {
//        public int? StudentId { get; set; }
//        public string? Username { get; set; }
//        public string? Email { get; set; }
//        public int?  Year { get; set; }
//        public List<Enrollments>? Enrollments { get; set; }

//        // Additional information
//        public string? RegistrationNo { get; set; }
//        public string? FullName { get; set; }
//        public int? CurrentSemester { get; set; }
//        public string? PhoneNo { get; set; }
//        public string? Address { get; set; }

//        //Parameterless constructor
//        public StudentDashboardViewModel()
//        {
//        }

//        // Constructor to map from Students entity
//        public StudentDashboardViewModel(Students student)
//        {
//            StudentId = student.Id;
//            Username = student.Name;
//            Email = student.Email;
//            Year = student.Year;
//            Enrollments = student.Enrollments;
//            RegistrationNo = student.RegistrationNo ?? "Not Provided";
//            FullName = student.FullName ?? "Unknown";
//            CurrentSemester = student.CurrentSemester;
//            PhoneNo = student.PhoneNo;
//            Address = student.Address ?? "N/A";
//        }
//    }
//}
///////########## 24th Jan addition######################
using System.Collections.Generic;

namespace Scholarly.Models
{
    public class StudentDashboardViewModel
    {
        public int? StudentId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public int? Year { get; set; }
        public List<Enrollments>? Enrollments { get; set; }

        // Additional information
        public string? RegistrationNo { get; set; }
        public string? FullName { get; set; }
        public int? CurrentSemester { get; set; }
        public string? PhoneNo { get; set; }
        public string? Address { get; set; }

        // Parameterless constructor
        public StudentDashboardViewModel()
        {
        }

        // Constructor to map from Students entity
        public StudentDashboardViewModel(Students student)
        {
            StudentId = student.Id;
            Username = student.Name;
            Email = student.Email;
            Year = student.Year;
            Enrollments = student.Enrollments;
            RegistrationNo = student.RegistrationNo ?? "Not Provided";
            FullName = student.FullName ?? "Unknown";
            CurrentSemester = student.CurrentSemester;
            PhoneNo = student.PhoneNo;
            Address = student.Address ?? "N/A";
        }
    }
}


