//using System.ComponentModel.DataAnnotations;

//namespace Scholarly.Models
//{
//    public class Students
//    {
//        public int Id { get; set; }
//        public string? Name { get; set; }    //UserName
//        public string? Email { get; set; }
//        public int? Year { get; set; }
//        public string? RegistrationNo { get; set; }
//        public string? FullName { get; set; }
//        public int? CurrentSemester { get; set; }
//        public string? PhoneNo { get; set; }
//        public string? Address { get; set; }
//        public List<Enrollments>? Enrollments { get; set; }
//    }


//}

////############### 24th Jan addition ############################
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scholarly.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string? Name { get; set; }    //UserName
        public string? Email { get; set; }
        public int? Year { get; set; }
        public string? RegistrationNo { get; set; }
        public string? FullName { get; set; }
        public int? CurrentSemester { get; set; }
        public string? PhoneNo { get; set; }
        public string? Address { get; set; }
        public List<Enrollments>? Enrollments { get; set; }

        // Foreign key to user
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public user User { get; set; }
    }
}

