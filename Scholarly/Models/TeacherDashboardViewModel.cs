//namespace Scholarly.Models
//{
//    public class TeacherDashboardViewModel
//    {
//        public int? TeacherId { get; set; }
//        public string? Username { get; set; }
//        public string? EmployeeRegistrationNo { get; set; }
//        public string? FullName { get; set; }
//        public string? PhoneNo { get; set; }
//        public string? Address { get; set; }
//        public List<Courses>? Courses { get; set; }
//    }
//}
////############### 24th Jan addition ############################
namespace Scholarly.Models
{
    public class TeacherDashboardViewModel
    {
        public int? TeacherId { get; set; }
        public string? Username { get; set; }
        public string? EmployeeRegistrationNo { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNo { get; set; }
        public string? Address { get; set; }
        public List<Courses>? Courses { get; set; }

        // Parameterless constructor
        public TeacherDashboardViewModel()
        {
        }

        // Constructor to map from Teachers entity
        public TeacherDashboardViewModel(Teachers teacher)
        {
            TeacherId = teacher.Id;
            Username = teacher.Name;
            EmployeeRegistrationNo = teacher.EmployeeRegistrationNo ?? "Not Provided";
            FullName = teacher.FullName ?? "Unknown";
            PhoneNo = teacher.PhoneNo;
            Address = teacher.Address ?? "N/A";
            Courses = teacher.Courses;
        }
    }
}

