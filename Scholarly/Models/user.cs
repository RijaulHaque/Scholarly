using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Scholarly.Models;

//#################### NEW CODE ################################
namespace Scholarly.Models
{
    //[Table("Users")]    // Ensure this matches your actual table name in the database
    public class user
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        
        [Required]
        public string Roles { get; set; }   // Later Added by Rijaul
    }
}

