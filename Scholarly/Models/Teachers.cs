//namespace Scholarly.Models
//{
//    public class Teachers
//    {
//        public int Id { get; set; }
//        public string? Name { get; set; }    //UserName
//        public string? EmployeeRegistrationNo { get; set; }
//        public string? FullName { get; set; }
//        public string? PhoneNo { get; set; }
//        public string? Address { get; set; }
//        public List<Courses>? Courses { get; set; }

//    }
//}
////############### 24th Jan addition ############################
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scholarly.Models
{
    public class Teachers
    {
        public int Id { get; set; }
        public string? Name { get; set; }    //UserName
        public string? EmployeeRegistrationNo { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNo { get; set; }
        public string? Address { get; set; }
        public List<Courses>? Courses { get; set; }

        // Foreign key to user
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public user User { get; set; }
    }
}
