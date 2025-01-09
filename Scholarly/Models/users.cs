using Microsoft.EntityFrameworkCore;
using Scholarly.Models;

namespace Scholarly.Models
{
    public class Users
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required Role role_id { get; set; }
        public required ICollection<Students> students_obj { get; set; }
        public required ICollection<Classes> Classes_obj { get; set; }
    }
}

public class Classes
{
    public required string class_id { get; set; }
    public required string class_name { get; set; }
    public required Users Id { get; set; }
    public required ICollection<Attendance> atttendance_obj { get; set; }
    public required ICollection<Enrollments> enrollments_obj { get; set; }
    public required ICollection<Grades> grades_obj { get; set; }
}

public class DatabaseContext : DbContext
{
    public DbSet<Users> user { get; set; }
}